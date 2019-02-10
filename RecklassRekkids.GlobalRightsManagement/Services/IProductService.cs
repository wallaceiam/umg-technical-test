using RecklassRekkids.GlobalRightsManagement.Filters;
using RecklassRekkids.GlobalRightsManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.GlobalRightsManagement.Services
{
    public interface IProductService
    {
        IEnumerable<MusicContract> GetMusicContracts(ProductFilter filter);
    }
}
