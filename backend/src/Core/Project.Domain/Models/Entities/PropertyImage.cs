

using Project.Infrastructure.Concretes;

namespace Project.Domain.Models.Entities
{
    public class PropertyImage:AuditableEntity
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Image { get; set; } // Store the file name
        public string Url { get; set; }   // Store the URL or path`
    }
}
