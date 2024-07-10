using Project.Infrastructure.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Models.Entities.Membership
{
    public class RolePolicy:AuditableEntity
    {
        public int RoleId { get; set; }
        public int PolicyId { get; set; }
        
    }
}
