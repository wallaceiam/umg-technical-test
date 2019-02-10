using RecklassRekkids.GlobalRightsManagement.Extensions;
using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecklassRekkids.GlobalRightsManagement.Filters
{
    public class ProductFilterBuilder
    {
        private const string FilterRegexPattern = @"([^\s]+?)\s+(.*)";

        public string Partner { get; protected set; }
        public DateTime EffectiveDate { get; protected set; }

        protected ProductFilterBuilder() { }

        public static ProductFilterBuilder From(string searchFilter)
        {
            var filter = searchFilter ?? throw new ArgumentNullException(nameof(searchFilter));

            var match = Regex.Match(filter.Trim(), FilterRegexPattern);

            if (!match.Success && match.Groups.Count != 3)
                throw new Exception($"Invalid search filter '${searchFilter}'. Please provide in the format '<partner> <effective date>'");

            var parnter = match.Groups[1].Value.Trim();
            var effectiveDate = match.Groups[2].Value.Trim().TryParseDate(out DateTime date) ? date 
                : throw new Exception($"Invalid effective data '${searchFilter}'. Please provide in the format 'd(st|nd|rd|th) MMM yyyy', ie '1st Mar 2019.");

            return From(parnter, effectiveDate);

        }

        public static ProductFilterBuilder From(string partner, DateTime effectiveDate)
        {
            var builder = new ProductFilterBuilder()
            {
                Partner = partner,
                EffectiveDate = effectiveDate
            };
            return builder;
        }
        
        public ProductFilter Build()
        {
            Func<MusicContract, bool> musicContractFilter = (m) =>
                m.StartDate <= EffectiveDate &&
                ((m.EndDate.HasValue && m.EndDate.Value >= EffectiveDate) || !m.EndDate.HasValue);

            Func<DistributionPartnerContract, bool> distributionPartnerContractFilter = (d) =>
                d.Partner.IsSameAs(Partner);

            return new ProductFilter(musicContractFilter, distributionPartnerContractFilter);
        }
    }
}