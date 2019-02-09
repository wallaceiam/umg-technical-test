using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.GlobalRightsManagement.Services
{
    public interface IProductService
    {
        IEnumerable<MusicContract> GetMusicContracts(Func<MusicContract, IEnumerable<DistributionPartnerContract>, bool> filter);
    }
}
