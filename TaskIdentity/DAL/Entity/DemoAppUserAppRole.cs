using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class DemoAppUserAppRole
    {
        public int Id { get; set; }

        [ForeignKey(nameof(AppUser))]
        public Guid AppUserId { get; set; }

        [ForeignKey(nameof(AppRole))]
        public Guid AppRoleId { get; set; }
        public DemoAppUser? AppUser { get; set; }
        public DemoAppRole? AppRole { get; set; }
    }
}
