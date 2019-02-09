using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecklassRekkids.GlobalRightsManagement.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}
