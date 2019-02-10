using FluentAssertions;
using RecklassRekkids.GlobalRightsManagement.Extensions;
using System;
using System.Collections.Generic;
using Xbehave;
using Xunit;

namespace RecklassRekkids.GlobalRightsManagement.Tests.Extensions
{
    public class DateTimeExtensionsFeature
    {
        [Scenario]
        [MemberData(nameof(TryParseDateData))]
        public void TryParseDateScenarios(string stringToTest, bool expectedValidity, DateTime expectedDateTime)
        {
            DateTime actualDateTime = DateTime.MinValue;
            bool actualValidity = false;

            $"Given {stringToTest}".x(() => { });
            "When TryParseDate is called"
                .x(() =>
                {
                    actualValidity = stringToTest.TryParseDate(out actualDateTime);
                });
            $"Then the output is {expectedDateTime:yyyy-MM-dd}"
                .x(() =>
                {
                    actualValidity.Should().Be(expectedValidity);
                    actualDateTime.Should().Be(expectedDateTime);
                });
        }

        [Scenario]
        [MemberData(nameof(AsOrdinalData))]
        public void AsOrdinalScenarios(int dayToTest, string expectedString)
        {
            var actualString = string.Empty;

            $"Given {dayToTest}".x(() => { });
            "When FormatDate is called"
                .x(() =>
                {
                    actualString = dayToTest.AsOrdinal();
                });
            $"Then the output is {expectedString}"
                .x(() =>
                {
                    actualString.Should().Be(expectedString);
                });
        }

        [Scenario]
        [MemberData(nameof(AsOrdinalExceptionData))]
        public void AsOrdinalOutOfRangeExceptionScenarios(int dayToTest)
        {
            $"Given {dayToTest}".x(() => { });
            "When FormatDate is called"
                .x(() =>
                {
                    dayToTest.Invoking(x => x.AsOrdinal()).Should().Throw<ArgumentOutOfRangeException>();
                });
            "Then exception should be thrown"
                .x(() => { });
        }

        [Scenario]
        [MemberData(nameof(FormatDateData))]
        public void FormatDateScenarios(DateTime dataTimeToTest, string expectedString)
        {
            var actualString = string.Empty;

            $"Given {dataTimeToTest:yyyy-MM-dd}".x(() => { });
            "When FormatDate is called"
                .x(() =>
                {
                    actualString = dataTimeToTest.FormatDate();
                });
            $"Then the output is {expectedString}"
                .x(() =>
                {
                    actualString.Should().Be(expectedString);
                });
        }

        [Scenario]
        [MemberData(nameof(FormatNullableDateData))]
        public void FormatNullableDateScenarios(DateTime? dataTimeToTest, string expectedString)
        {
            var actualString = string.Empty;

            $"Given {dataTimeToTest:yyyy-MM-dd}".x(() => { });
            "When FormatDate is called"
                .x(() =>
                {
                    actualString = dataTimeToTest.FormatDate();
                });
            $"Then the output is {expectedString}"
                .x(() =>
                {
                    actualString.Should().Be(expectedString);
                });
        }

        #region Data

        public static IEnumerable<object[]> TryParseDateData =>
           new List<object[]>
           {
                new object[] { "1st March 2012", true, new DateTime(2012, 03, 01) },
                new object[] { "2nd March 2012", true, new DateTime(2012, 03, 02) },
                new object[] { "3rd March 2012", true, new DateTime(2012, 03, 03) },
                new object[] { "4th March 2012", true, new DateTime(2012, 03, 04) },
                new object[] { "2nd Dec 2012", true, new DateTime(2012, 12, 02) },

                new object[] { "Mar 2nd 2012", false, DateTime.MinValue },

                new object[] { "Foo Bar Bizz", false, DateTime.MinValue },

           };

        public static IEnumerable<object[]> AsOrdinalData =>
            new List<object[]>
            {
                new object[] { 1, "1st", },
                new object[] { 2, "2nd" },
                new object[] { 3, "3rd" },
                new object[] { 4, "4th" },
                new object[] { 5, "5th" },
                new object[] { 6, "6th" },
                new object[] { 7, "7th" },
                new object[] { 8, "8th" },
                new object[] { 9, "9th" },
                new object[] { 10, "10th" },
                new object[] { 11, "11th" },
                new object[] { 12, "12th" },
                new object[] { 13, "13th" },
                new object[] { 14, "14th" },
            };

        public static IEnumerable<object[]> AsOrdinalExceptionData =>
            new List<object[]>
            {
                new object[] { -1 },
                new object[] { 0, },
                new object[] { 32 },
            };

        public static IEnumerable<object[]> FormatDateData =>
            new List<object[]>
            {
                new object[] { new DateTime(2012, 03, 01), "1st Mar 2012", },
                new object[] { new DateTime(2012, 03, 02), "2nd Mar 2012" },
                new object[] { new DateTime(2012, 03, 03), "3rd Mar 2012" },
                new object[] { new DateTime(2012, 03, 04), "4th Mar 2012" },

                new object[] { new DateTime(2012, 06, 04), "4th June 2012" },
                new object[] { new DateTime(2012, 07, 04), "4th July 2012" },
            };

        public static IEnumerable<object[]> FormatNullableDateData =>
            new List<object[]>
            {
                new object[] { new DateTime(2012, 03, 01), "1st Mar 2012", },
                new object[] { null, "" }
            };

        #endregion
    }
}
