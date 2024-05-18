using Project.Infrastructure.Concretes;


namespace Project.Domain.Models.Entities
{
    public class Description : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Explanation { get; set; }
    }
}
