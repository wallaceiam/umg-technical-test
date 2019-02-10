using RecklassRekkids.GlobalRightsManagement.Extensions;
using RecklassRekkids.GlobalRightsManagement.Filters;
using RecklassRekkids.GlobalRightsManagement.Models;
using RecklassRekkids.GlobalRightsManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecklassRekkids.GlobalRightsManagement.Services
{
    public class ProductSearchService : IProductService
    {
        private readonly IRepository<DistributionPartnerContract> _distributionPartnerContractsRepo;
        private readonly IRepository<MusicContract> _musicContractsRepo;
        public ProductSearchService(IRepository<DistributionPartnerContract> distributionPartnerContractsRepo, IRepository<MusicContract> musicContractsRepo)
        {
            _distributionPartnerContractsRepo = distributionPartnerContractsRepo ?? throw new ArgumentNullException(nameof(distributionPartnerContractsRepo));
            _musicContractsRepo = musicContractsRepo ?? throw new ArgumentNullException(nameof(musicContractsRepo));
        }
        public IEnumerable<MusicContract> GetMusicContracts(ProductFilter filter)
        {
            var availableContracts = _musicContractsRepo.GetAll();
            if (filter != null)
            {
                // I have left this as two separate linq queries for readability
                
                var distributionPartnerContracts = _distributionPartnerContractsRepo.GetAll()
                    .Where(d => filter.DistributionParnterContractFilter(d))
                    .Select(d => d.Usage)
                    .Distinct();

                availableContracts = availableContracts
                    .Where(m => m.Usages.Any(u => distributionPartnerContracts.Any(d => u.IsSameAs(d))))
                    .Where(m => filter.MusicContractFilter(m));
           }

            return availableContracts
                .OrderBy(x => x.Artist)
                .ThenByDescending(x => x.StartDate)
                .ToList();
        }
    }
}
