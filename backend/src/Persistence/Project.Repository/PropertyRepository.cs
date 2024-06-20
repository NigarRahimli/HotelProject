using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;
using Location = Project.Domain.Models.Entities.Location;

namespace Project.Repository
{
    public class PropertyRepository : AsyncRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(DbContext db) : base(db)
        {
            
        }

        public IQueryable<Property> GetNearbyPropertiesAsync(Point userLocation)
        {
            var propertiesWithLocations = db.Set<Property>()
                .Join(db.Set<Location>(),
                      property => property.LocationId,
                      location => location.Id,
                      (property, location) => new { property, location })
                .Where(x => x.location.Coordinates.IsWithinDistance(userLocation, 1000000))
                .OrderBy(x => x.location.Coordinates.Distance(userLocation))
                .Select(x => x.property)
               .AsQueryable();


            return propertiesWithLocations;
        }

    }
}
