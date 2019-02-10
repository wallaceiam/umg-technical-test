using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklassRekkids.GlobalRightsManagement.Tests.Data
{
    internal static class StaticData
    {
        internal static IEnumerable<MusicContract> MusicContracts =>
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

        internal static IEnumerable<DistributionPartnerContract> DistributionPartnerContracts =>
            new List<DistributionPartnerContract>
            {
                new DistributionPartnerContract() { Partner = "ITunes", Usage = "digital download"},
                new DistributionPartnerContract() { Partner = "YouTube", Usage = "streaming"},
            };
    }
}
