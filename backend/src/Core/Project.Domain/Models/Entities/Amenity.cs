
using Project.Infrastructure.Concretes;

namespace Project.Domain.Models.Entities
{
    public class Amenity:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }

    }
}
