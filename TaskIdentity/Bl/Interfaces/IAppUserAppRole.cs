using Bl.Repository;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Interfaces
{
    public interface IAppUserAppRole : IGenericRepository<DemoAppUserAppRole>
    {
    
       Task<DemoAppUserAppRole> GetBy(Guid userId, Guid RoleId);
    }
}
