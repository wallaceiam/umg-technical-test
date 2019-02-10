using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecklassRekkids.GlobalRightsManagement.Filters
{
    public class ProductFilter
    {
        public Func<MusicContract, bool> MusicContractFilter { get; protected set; }
        public Func<DistributionPartnerContract, bool> DistributionParnterContractFilter { get; protected set; }

        public ProductFilter(Func<MusicContract, bool> musicContractFilter,
            Func<DistributionPartnerContract, bool> distributionParnterContractFilter)
        {
            MusicContractFilter = musicContractFilter ?? throw new ArgumentNullException(nameof(musicContractFilter));
            DistributionParnterContractFilter = distributionParnterContractFilter ?? throw new ArgumentNullException(nameof(distributionParnterContractFilter));
        }

    }
}
