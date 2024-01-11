using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AppContext
{
    public class DemoContext : DbContext
    {


        public DbSet<DemoAppUser> AppUser { get; set; }
        public DbSet<DemoAppRole> AppRole { get; set; }
        public DbSet<DemoAppUserAppRole> AppUserAppRole { get; set; }
        public DemoContext(DbContextOptions<DemoContext> options):base(options) { }
      
    }
}
