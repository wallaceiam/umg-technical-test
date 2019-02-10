using FluentAssertions;
using RecklassRekkids.GlobalRightsManagement.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xbehave;
using Xunit;

namespace RecklassRekkids.GlobalRightsManagement.Tests.Extensions
{
    public class StringExtensionsFeature
    {
        [Scenario]
        [MemberData(nameof(IsSameAsData))]
        public void IsSameAsScenarios(string stringToTest, string stringToCompare, bool expectedValidity)
        {
            bool actualValidity = false;

            $"Given {stringToTest}".x(() => { });
            "When IsSameAs is called"
                .x(() =>
                {
                    actualValidity = stringToTest.IsSameAs(stringToCompare);
                });
            $"Then the output is {expectedValidity}"
                .x(() =>
                {
                    actualValidity.Should().Be(expectedValidity);
                });
        }

        #region Data 

        public static IEnumerable<object[]> IsSameAsData =>
           new List<object[]>
           {
                new object[] { "Test", "Test", true },
                new object[] { "tEst", "Test", true },
                new object[] { "TEST", "test", true },
                new object[] { "Foo", "Bar", false },
                new object[] { "Fizz", "Biz", false },
                new object[] { "", "Biz", false },
                new object[] { "", "", true },
                new object[] { null, null, true },
                new object[] { null, "Test", false },
                new object[] { "Test", null, false },
           };

        #endregion
    }
}
