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
    public class AppUserRepository :  GenericRepository<DemoAppUser> , IAppUser
    {
        private readonly DemoContext context;

        public AppUserRepository(DemoContext context):base(context)
        {
            //var test = new Test() { Name = "test" };
            //context.tests.Add(test);
            //context.SaveChanges();
            this.context = context;
        }



    }
}
