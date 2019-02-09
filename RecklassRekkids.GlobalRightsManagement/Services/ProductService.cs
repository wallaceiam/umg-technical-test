using RecklassRekkids.GlobalRightsManagement.Models;
using RecklassRekkids.GlobalRightsManagement.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.GlobalRightsManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<DistributionPartnerContract> _distributionPartnerContractsRepo;
        private readonly IRepository<MusicContract> _musicContractsRepo;
        public ProductService(IRepository<DistributionPartnerContract> distributionPartnerContractsRepo, IRepository<MusicContract> musicContractsRepo)
        {
            _distributionPartnerContractsRepo = distributionPartnerContractsRepo ?? throw new ArgumentNullException(nameof(distributionPartnerContractsRepo));
            _musicContractsRepo = musicContractsRepo ?? throw new ArgumentNullException(nameof(musicContractsRepo));
        }
        public  IEnumerable<MusicContract> GetMusicContracts(Func<MusicContract, IEnumerable<DistributionPartnerContract>, bool> filter)
        {
            var distributionPartnerContracts = _distributionPartnerContractsRepo.GetAll();

            var availableContracts = _musicContractsRepo.GetAll()
                .Where(x => filter(x, distributionPartnerContracts))
                .OrderBy(x => x.Artist)
                .ThenByDescending(x => x.StartDate);

            return availableContracts.ToList();
        }
    }
}
