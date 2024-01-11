using Bl.Interfaces;
using DAL.AppContext;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Repository
{
    public class AppUserAppRoleRepository : GenericRepository<DemoAppUserAppRole> ,  IAppUserAppRole  
    {
        private readonly DemoContext context;

        public AppUserAppRoleRepository(DemoContext context) :base(context) 
        {
            this.context = context;
        }
        public async Task<DemoAppUserAppRole> GetBy(Guid userId, Guid RoleId)
        {
            return await context.AppUserAppRole.SingleOrDefaultAsync(i => i.AppUserId == userId && i.AppRoleId == RoleId);
        }


    }
}
