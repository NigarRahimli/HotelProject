
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;
using Project.Infrastructure.Exceptions;


namespace Project.Repository
{
    public class PropertyRepository : AsyncRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(DbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Property>> GetPropertiesWithMostFeatured(int take, CancellationToken cancellationToken)
        {
            var propertiesWithMostFacilities = await db.Set<FacilityCount>()
                .GroupBy(fc => fc.PropertyId)
                .Select(g => new
                {
                    PropertyId = g.Key,
                    FacilityCount = g.Count(),
                    FacilitySumCount = g.Sum(fc => fc.Count)
                })
                .OrderByDescending(p => p.FacilityCount)
                .ThenByDescending(p => p.FacilitySumCount)
                .Take(take)
                .ToListAsync(cancellationToken);

            if (!propertiesWithMostFacilities.Any())
            {
                throw new NotFoundException($"{nameof(Property)} not found");
            }

            var propertyIds = propertiesWithMostFacilities.Select(p => p.PropertyId).ToList();
            var properties = await db.Set<Property>()
                .Where(p => propertyIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            var sortedProperties = propertyIds.Select(id => properties.First(p => p.Id == id)).ToList();

            return sortedProperties;
        }
    
    public IEnumerable<Property> GetNearbyProperties(double latitude, double longitude, int maxDistanceMeters, int take)
        {
           
            var lat1 = latitude * Math.PI / 180.0;
            var lon1 = longitude * Math.PI / 180.0;

            
            var properties = db.Set<Property>()
                .Join(db.Set<Location>(),
                      property => property.LocationId,
                      location => location.Id,
                      (property, location) => new { Property = property, Location = location })
                .AsEnumerable(); 

            
            var nearbyProperties = properties
                .Select(x => new
                {
                    Property = x.Property,
                    Distance = CalculateDistance(lat1, lon1, x.Location.Latitude * Math.PI / 180.0, x.Location.Longitude * Math.PI / 180.0)
                })
                .Where(x => x.Distance <= maxDistanceMeters)
                .OrderBy(x => x.Distance)
                .Take(take)
                .Select(x => x.Property);

            return nearbyProperties;
        }

        // Helper method to calculate distance between two points using Haversine formula
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            
            const double earthRadius = 6371000;

            var dLat = lat2 - lat1;
            var dLon = lon2 - lon1;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = earthRadius * c;

            return distance;
        }

    }
}
