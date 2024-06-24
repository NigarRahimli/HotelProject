using Project.Infrastructure.Concretes;

namespace Project.Domain.Models.Entities
{
    public class Review:AuditableEntity
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int CategoryId { get; set; }
        public int Stars { get; set; }
    }
}
