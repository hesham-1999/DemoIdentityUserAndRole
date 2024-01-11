using Bl.Interfaces;
using DAL.AppContext;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Repository
{
    public class AppRoleRepository : GenericRepository<DemoAppRole> ,IAppRole
    {
        public AppRoleRepository(DemoContext context) : base(context)
        {
        }
    }
}
