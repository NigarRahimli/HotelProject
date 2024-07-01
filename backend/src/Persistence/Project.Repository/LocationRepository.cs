using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.LocationsModule;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;
using Project.Infrastructure.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class LocationRepository : AsyncRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext db) : base(db)
        {
        }

        public async Task<LocationDetailDto> GetLocationDetailsAsync(int locationId, CancellationToken cancellationToken)
        {
            var location = await db.Set<Location>()
                .Where(l => l.Id == locationId && l.DeletedBy == null)
                .FirstOrDefaultAsync(cancellationToken);

            if (location == null)
            {
                throw new NotFoundException($"{nameof(Location)} not found.");
            }

            return new LocationDetailDto
            {
                City = location.City,
                Country = location.Country,
                Address = location.Address
            };
        }
    }
}
