using Project.Infrastructure.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Models.Entities
{
    public class ReviewCategory:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
