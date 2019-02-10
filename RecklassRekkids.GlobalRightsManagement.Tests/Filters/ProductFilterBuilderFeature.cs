using FluentAssertions;
using RecklassRekkids.GlobalRightsManagement.Filters;
using RecklassRekkids.GlobalRightsManagement.Models;
using RecklassRekkids.GlobalRightsManagement.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xbehave;
using Xunit;

namespace RecklassRekkids.GlobalRightsManagement.Tests.Filters
{
    public class ProductFilterBuilderFeature
    {
        [Scenario]
        public void FromPartnerAndEffectiveDateScenario()
        {
            var partner = "PARTNER";
            var effectiveDate = new DateTime(2019, 02, 19);

            var actualPartner = "";
            var actualEffectiveDate = DateTime.MinValue;

            $"Given {partner} and {effectiveDate:yyyy-MM-dd}".x(() => { });
            "When From is called"
                .x(() =>
                {
                    var builder = ProductFilterBuilder.From(partner, effectiveDate);
                    actualPartner = builder.Partner;
                    actualEffectiveDate = builder.EffectiveDate;
                });
            "Then actual partner and effective date should match expected"
                .x(() =>
                {
                    actualEffectiveDate.Should().Be(effectiveDate);
                    actualPartner.Should().Be(partner);
                });
        }

        [Scenario]
        [MemberData(nameof(FromSearchFilterScenarioData))]
        public void FromSearchFilterScenario(string searchFilter, string partner, DateTime effectiveDate)
        {
            var actualPartner = "";
            var actualEffectiveDate = DateTime.MinValue;

            $"Given {searchFilter}".x(() => { });
            "When From is called"
                .x(() =>
                {
                    var builder = ProductFilterBuilder.From(searchFilter);
                    actualPartner = builder.Partner;
                    actualEffectiveDate = builder.EffectiveDate;
                });
            "Then actual partner and effective date should match expected"
                .x(() =>
                {
                    actualEffectiveDate.Should().Be(effectiveDate);
                    actualPartner.Should().Be(partner);
                });
        }

        [Scenario]
        [MemberData(nameof(FromSearchFilterExceptionScenarioData))]
        public void FromSearchFilterExceptionScenario(string searchFilter)
        {

            $"Given {searchFilter}".x(() => { });
            "When From is called"
                .x(() =>
                {
                    Action a = () => ProductFilterBuilder.From(searchFilter);
                    a.Should().Throw<Exception>();
                });
            "Then exception should be thrown"
                .x(() => { });
        }

        [Scenario]
        [MemberData(nameof(BuildDistributionPatnerChannelFilterScenarioData))]
        public void BuildDistributionPatnerChannelFilterScenario(string partner, IEnumerable<DistributionPartnerContract> distributionPartnerContracts, int expectedCount)
        {
            var effectiveDate = new DateTime(2019, 02, 19);

            var actualCount = 0;

            $"Given {partner} and {effectiveDate:yyyy-MM-dd}".x(() => { });
            "When DistributionParnterContractFilter is called"
                .x(() =>
                {
                    var filter = ProductFilterBuilder.From(partner, effectiveDate).Build();
                    actualCount = distributionPartnerContracts.Where(d => filter.DistributionParnterContractFilter(d)).Count();
                });
            "Then actual count should match "
                .x(() =>
                {
                    actualCount.Should().Be(expectedCount);
                });
        }

        [Scenario]
        [MemberData(nameof(BuildMusicChannelFilterScenarioData))]
        public void BuildMusicChannelFilterScenario(DateTime dateTime, IEnumerable<MusicContract> musicContracts, int expectedCount)
        {
            var partner = "PARTNER";
            var actualCount = 0;

            $"Given {partner} and {dateTime:yyyy-MM-dd}".x(() => { });
            "When MusicContractFilter is called"
                .x(() =>
                {
                    var filter = ProductFilterBuilder.From(partner, dateTime).Build();
                    actualCount = musicContracts.Where(m => filter.MusicContractFilter(m)).Count();
                });
            "Then actual count should match "
                .x(() =>
                {
                    actualCount.Should().Be(expectedCount);
                });
        }

        #region Data

        public static IEnumerable<object[]> FromSearchFilterScenarioData =>
           new List<object[]>
           {
                new object[] { "ITunes 1st March 2019", "ITunes", new DateTime(2019, 03, 01) },
                new object[] { "Vimeo 1st March 2019", "Vimeo", new DateTime(2019, 03, 01) },
                new object[] { "YouTube 19th March 2019", "YouTube", new DateTime(2019, 03, 19) },
                new object[] { "YouTube 19th Mar 2019", "YouTube", new DateTime(2019, 03, 19) },
                new object[] { "            YouTube 19th Mar 2019", "YouTube", new DateTime(2019, 03, 19) },
                new object[] { "-_-asdYouTube 19th Mar 2019", "-_-asdYouTube", new DateTime(2019, 03, 19) },
                new object[] { "YouTube                     19th Mar 2019", "YouTube", new DateTime(2019, 03, 19) },
                new object[] { "YouTube                     19th March 2019               ", "YouTube", new DateTime(2019, 03, 19) },
           };

        public static IEnumerable<object[]> FromSearchFilterExceptionScenarioData =>
          new List<object[]>
          {
                new object[] { "1st March 2019" },
                new object[] { "Vimeo 2019-01-01" },
                new object[] { "Vimeo 1st March" },
                new object[] { "1st Mar 2019 Vimeo" },
                new object[] { "Fizz Buz Fiz Buz" },
                new object[] { "-p Fizz -e2019-01-01" },
                new object[] { "" },
                new object[] { null },
          };

        public static IEnumerable<object[]> BuildDistributionPatnerChannelFilterScenarioData =>
            new List<object[]>
            {
                new object[] { "PARTNER", StaticData.DistributionPartnerContracts, 0 },
                new object[] { "ITunes", StaticData.DistributionPartnerContracts, 1 },
                new object[] { "iTunes", StaticData.DistributionPartnerContracts, 1 },
                new object[] { null, StaticData.DistributionPartnerContracts, 0 },
            };

        public static IEnumerable<object[]> BuildMusicChannelFilterScenarioData =>
            new List<object[]>
            {
                new object[] { new DateTime(1900, 01, 01), StaticData.MusicContracts, 0 },
                new object[] { DateTime.MaxValue, StaticData.MusicContracts, 6 },
                new object[] { DateTime.MinValue, StaticData.MusicContracts, 0 },
                new object[] { new DateTime(2012, 02, 01), StaticData.MusicContracts, 4 },
                new object[] { new DateTime(2012, 12, 26), StaticData.MusicContracts, 7 },
                new object[] { new DateTime(2012, 12, 25), StaticData.MusicContracts, 7 },
                new object[] { new DateTime(2012, 12, 31), StaticData.MusicContracts, 7 },

            };


        #endregion
    }
}
