using Project.Infrastructure.Concretes;


namespace Project.Domain.Models.Entities
{
    public class ReviewCategory:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
