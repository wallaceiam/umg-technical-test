using System;
using Xunit;
using Xbehave;
using FluentAssertions;

namespace RecklassRekkids.GlobalRightsManagement.Tests
{
    public class SearchFeature
    {
        [Scenario]
        [Example("ITunes 1st March 2012", 4)]
        [Example("YouTube 1st April 2012", 2)]
        [Example("YouTube 27th Dec 2012", 4)]
        public void ClientProvided1(string searchTerm, int expectedNumberOfResults)
        {
            "Given the supplied aboive reference data"
                .x(() => { });

            $"When user enters '{searchTerm}'"
                .x(() => { });

            "Then the output is"
                .x(() =>
                {

                });
        }

    }
}
