using FluentAssertions;
using Moq;
using RecklassRekkids.GlobalRightsManagement.Filters;
using RecklassRekkids.GlobalRightsManagement.Models;
using RecklassRekkids.GlobalRightsManagement.Repositories;
using RecklassRekkids.GlobalRightsManagement.Services;
using RecklassRekkids.GlobalRightsManagement.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xbehave;
using Xunit;

namespace RecklassRekkids.GlobalRightsManagement.Tests.Services
{
    public class ProductSearchServiceFeature
    {
        [Scenario]
        public void ConstructorExceptionScenario()
        {
            "Given".x(() => { });
            "When constructor is called with null parameters"
                .x(() =>
                {
                    var musicContractRepo = new Mock<IRepository<MusicContract>>();
                    var distPartnerContractRepo = new Mock<IRepository<DistributionPartnerContract>>();

                    Action a = () => new ProductSearchService(null, null);
                    a.Should().Throw<ArgumentNullException>();
                    a = () => new ProductSearchService(distPartnerContractRepo.Object, null);
                    a.Should().Throw<ArgumentNullException>();
                    a = () => new ProductSearchService(null, musicContractRepo.Object);
                    a.Should().Throw<ArgumentNullException>();
                });
            "Then exception should be thrown"
                .x(() => { });
        }

        [Scenario]
        public void GetMusicContractsNoFilterScenario()
        {
            var musicContractRepo = new Mock<IRepository<MusicContract>>();
            var distPartnerContractRepo = new Mock<IRepository<DistributionPartnerContract>>();

            var searchService = new ProductSearchService(distPartnerContractRepo.Object, musicContractRepo.Object);

            var actualCount = 0;
            var expectedCount = StaticData.MusicContracts.Count();

            "Given no filter is provided".x(() => {
                musicContractRepo.Setup(r => r.GetAll()).Returns(StaticData.MusicContracts);
            });
            "When GetMusicContracts is called"
                .x(() =>
                {
                    actualCount = searchService.GetMusicContracts(null).Count();
                });
            "Then all music contracts should be returned"
                .x(() => {
                    actualCount.Should().Be(expectedCount);
                });
        }

        [Scenario]
        [MemberData(nameof(GetMusicContractsData))]
        public void GetMusicContractsScenario(string filterText, IEnumerable<MusicContract> musicContracts,
            IEnumerable<DistributionPartnerContract> distributionPartnerContracts, string expectedSongs)
        {
            var musicContractRepo = new Mock<IRepository<MusicContract>>();
            var distPartnerContractRepo = new Mock<IRepository<DistributionPartnerContract>>();

            var searchService = new ProductSearchService(distPartnerContractRepo.Object, musicContractRepo.Object);

            var actualSongs = "";
            ProductFilter filter = null;

            $"Given a filter '{filterText} is provided".x(() => {
                musicContractRepo.Setup(r => r.GetAll()).Returns(musicContracts);
                distPartnerContractRepo.Setup(r => r.GetAll()).Returns(distributionPartnerContracts);

                filter = ProductFilterBuilder.From(filterText).Build();
            });
            "When GetMusicContracts is called"
                .x(() =>
                {
                    var results = searchService.GetMusicContracts(filter);
                    actualSongs = string.Join(',', results.Select(x => x.Title));
                });
            "Then all music contracts should be returned"
                .x(() => {
                    actualSongs.Should().Be(expectedSongs);
                });
        }


        #region Data 

        public static IEnumerable<object[]> GetMusicContractsData =>
           new List<object[]>
           {
                new object[] { "ITunes 1st March 2012", StaticData.MusicContracts, StaticData.DistributionPartnerContracts, "Black Mountain,Motor Mouth,Frisky (Live from SoHo),Miami 2 Ibiza" },
                new object[] { "YouTube 1st April 2012", StaticData.MusicContracts, StaticData.DistributionPartnerContracts, "Motor Mouth,Frisky (Live from SoHo)" },
                new object[] { "YouTube 27th Dec 2012", StaticData.MusicContracts, StaticData.DistributionPartnerContracts, "Christmas Special,Iron Horse,Motor Mouth,Frisky (Live from SoHo)" },
                new object[] { "Vimeo 27th Dec 2012", StaticData.MusicContracts, StaticData.DistributionPartnerContracts, "" },
                new object[] { "YouTube 27th Dec 1900", StaticData.MusicContracts, StaticData.DistributionPartnerContracts, "" },

           };

        #endregion
    }
}
