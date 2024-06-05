using Project.Infrastructure.Concretes;

namespace Project.Domain.Models.Entities
{
    public class FacilityCount : AuditableEntity
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int FacilityId { get; set; }
        public int Count { get; set; }
    }
}
