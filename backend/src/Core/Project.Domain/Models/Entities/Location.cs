using NetTopologySuite.Geometries;
using Project.Infrastructure.Concretes;

namespace Project.Domain.Models.Entities
{
    public class Location:AuditableEntity
    {
        public int Id { get; set; }
        public Point Coordinates { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
