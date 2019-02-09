using RecklassRekkids.GlobalRightsManagement.Extensions;
using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecklassRekkids.GlobalRightsManagement.Services
{
    public class ProductFilterBuilder
    {
        private const string FilterRegexPattern = @"([^\s]+?)\s+(.*)";

        private Func<MusicContract, IEnumerable<DistributionPartnerContract>, bool> _filter;

        protected ProductFilterBuilder() { }

        public static ProductFilterBuilder ParseCommand(string searchFilter)
        {
            var match = Regex.Match(searchFilter, FilterRegexPattern);

            if (!match.Success && match.Groups.Count != 3)
                throw new Exception($"Invalid filter ${searchFilter}");

            DateTime date;
            var partner = match.Groups[1].Value.Trim();
            var effectiveDate = match.Groups[2].Value.TryParseDate(out date) ? date : throw new Exception($"Invalid filter ${searchFilter}");

            var filter = new ProductFilterBuilder();
            filter._filter = (m, d) =>
            {
                var dd = d
                    .Where(x => string.Compare(x.Partner, partner, true, CultureInfo.InvariantCulture) == 0)
                    .Select(x => x.Usage);

                return 
                    m.Usages.Any(u => dd.Any(uu => string.Compare(u, uu, true, CultureInfo.InvariantCulture) == 0)) &&
                    m.StartDate <= effectiveDate &&
                    ((m.EndDate.HasValue && m.EndDate.Value >= effectiveDate) || !m.EndDate.HasValue);
            };
            return filter;
        }

        public Func<MusicContract, IEnumerable<DistributionPartnerContract>, bool> Build()
        {
            return _filter;
        }
    }
}