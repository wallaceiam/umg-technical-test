using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecklassRekkids.GlobalRightsManagement.Models;
using RecklassRekkids.GlobalRightsManagement.Repositories;
using RecklassRekkids.GlobalRightsManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xbehave;
using Xunit;

namespace RecklassRekkids.GlobalRightsManagement.Tests
{
    public class ClientAcceptanceCriteria
    {
        [Scenario]
        [MemberData(nameof(ClientProvidedScenarioData))]
        public void ClientProvidedScenarios(string filter, IEnumerable<MusicContract> musicContracts,
            IEnumerable<DistributionPartnerContract> distributionPartnerContracts, string expectedSongs)
        {

            var mockMusicContractRepo = new Mock<IRepository<MusicContract>>();
            var mockDistributionPartnerContractFileRepository = new Mock<IRepository<DistributionPartnerContract>>();
            var mockConsoleUI = new Mock<IFancyUI>();

            var serviceProvider = new ServiceCollection()
                    .AddTransient<IRepository<MusicContract>>(x => mockMusicContractRepo.Object)
                    .AddTransient<IRepository<DistributionPartnerContract>>(x => mockDistributionPartnerContractFileRepository.Object)
                    .AddTransient<IFancyUI>(x => mockConsoleUI.Object)
                    .AddTransient<IProductService, ProductSearchService>()
                    .BuildServiceProvider();

            Application application = Application.Build(serviceProvider);

            var actualSongs = string.Empty;

            "Given the supplied above reference data"
                .x(() =>
                {
                    mockMusicContractRepo.Setup(x => x.GetAll()).Returns(musicContracts);
                    mockDistributionPartnerContractFileRepository.Setup(x => x.GetAll()).Returns(distributionPartnerContracts);

                    // We are just going to capture the songs to assert them later
                    mockConsoleUI
                    .Setup(x => x.Display(It.IsAny<IEnumerable<MusicContract>>()))
                    .Callback<IEnumerable<MusicContract>>((x) =>
                    {
                        actualSongs = string.Join(',', x.Select(y => y.Title));
                    });
                });
            $"When user enters '{filter}'"
                .x(() => { application.Run(new string[] { filter }); });
            $"Then the output is {expectedSongs}"
                .x(() =>
                {
                    // if this was a production system then we should be asserting a lot more than just the song name
                    actualSongs.Should().Be(expectedSongs);
                });
        }


        #region Data 

        private static IEnumerable<MusicContract> MusicContracts =>
            new List<MusicContract>
            {
               new MusicContract() { Artist = "Tinie Tempah", Title = "Frisky (Live from SoHo)", Usages = new List<string>() { "digital download", "streaming" }, StartDate = new DateTime(2012, 02, 01), EndDate = null },
               new MusicContract() { Artist = "Tinie Tempah", Title = "Miami 2 Ibiza", Usages = new List<string>() { "digital download" }, StartDate = new DateTime(2012, 02, 01), EndDate = null },
               new MusicContract() { Artist = "Tinie Tempah", Title = "Till I'm Gone", Usages = new List<string>() { "digital download" }, StartDate = new DateTime(2012, 08, 01), EndDate = null },

               new MusicContract() { Artist = "Monkey Claw", Title = "Black Mountain", Usages = new List<string>() { "digital download" }, StartDate = new DateTime(2012, 02, 01), EndDate = null },
               new MusicContract() { Artist = "Monkey Claw", Title = "Iron Horse", Usages = new List<string>() { "digital download", "streaming" }, StartDate = new DateTime(2012, 06, 01), EndDate = null },
               new MusicContract() { Artist = "Monkey Claw", Title = "Motor Mouth", Usages = new List<string>() { "digital download", "streaming" }, StartDate = new DateTime(2011, 03, 01), EndDate = null },
               new MusicContract() { Artist = "Monkey Claw", Title = "Christmas Special", Usages = new List<string>() { "streaming" }, StartDate = new DateTime(2012, 12, 25), EndDate = new DateTime(2012, 12, 31) },
            };

        public static IEnumerable<DistributionPartnerContract> DistributionPartnerContracts =>
            new List<DistributionPartnerContract>
            {
                new DistributionPartnerContract() { Partner = "ITunes", Usage = "digital download"},
                new DistributionPartnerContract() { Partner = "YouTube", Usage = "streaming"},
            };

        public static IEnumerable<object[]> ClientProvidedScenarioData =>
           new List<object[]>
           {
                new object[] { "ITunes 1st March 2012", MusicContracts, DistributionPartnerContracts, "Black Mountain,Motor Mouth,Frisky (Live from SoHo),Miami 2 Ibiza" },
                new object[] { "YouTube 1st April 2012", MusicContracts, DistributionPartnerContracts, "Motor Mouth,Frisky (Live from SoHo)" },
                new object[] { "YouTube 27th Dec 2012", MusicContracts, DistributionPartnerContracts, "Christmas Special,Iron Horse,Motor Mouth,Frisky (Live from SoHo)" },

           };

        #endregion

    }
}
