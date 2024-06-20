using NetTopologySuite.Geometries;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;


namespace Project.Application.Repositories
{
    public interface IPropertyRepository : IAsyncRepository<Property>
    {
        IQueryable<Property> GetNearbyPropertiesAsync(Point userLocation);
    }
}
