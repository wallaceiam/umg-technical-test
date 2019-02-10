using FluentAssertions;
using RecklassRekkids.GlobalRightsManagement.Extensions;
using RecklassRekkids.GlobalRightsManagement.Models;
using RecklassRekkids.GlobalRightsManagement.Filters;
using System;
using System.Collections.Generic;
using Xbehave;
using Xunit;

namespace RecklassRekkids.GlobalRightsManagement.Tests.Filters
{
    public class ProductFilterFeature
    {
        [Scenario]
        public void ProductFilterConstructorExceptions()
        {
            "Given".x(() => { });
            "When constructor is called with null parameters"
                .x(() =>
                {
                    Action a = () => new ProductFilter(null, null);
                    a.Should().Throw<ArgumentNullException>();
                    a = () => new ProductFilter(null, (d) => true);
                    a.Should().Throw<ArgumentNullException>();
                    a = () => new ProductFilter((m) => true, null);
                    a.Should().Throw<ArgumentNullException>();
                });
            "Then exception should be thrown"
                .x(() => { });
        }

        [Scenario]
        [MemberData(nameof(ProductFilterShouldRetainFunctionsData))]
        public void ProductFilterShouldRetainFunctions(bool musicContractResult, bool distPartnerContractResult, bool expectedMusicContractResult, bool expectedDistPartnerContractResult)
        {
            ProductFilter productFilter = null;
            bool actualMusicContractResult = false;
            bool actualDistPartnerContractResult = false;

            "Given a basic ProductFilter".x(() =>
            {
                productFilter = new ProductFilter((m) => musicContractResult, (d) => distPartnerContractResult);
            });
            "When Filters are called"
                .x(() =>
                {
                    actualDistPartnerContractResult = productFilter.DistributionParnterContractFilter(new DistributionPartnerContract());
                    actualMusicContractResult = productFilter.MusicContractFilter(new MusicContract());
                });
            "Then expected result should match the actual"
                .x(() =>
                {
                    actualDistPartnerContractResult.Should().Be(expectedDistPartnerContractResult);
                    actualMusicContractResult.Should().Be(expectedMusicContractResult);
                });
        }

        #region Data

        public static IEnumerable<object[]> ProductFilterShouldRetainFunctionsData =>
           new List<object[]>
           {
                new object[] { true, true, true, true },
                new object[] { false, true, false, true },
                new object[] { true, false, true, false },
                new object[] { false, false, false, false },

           };

        #endregion
    }
}
