using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.LocationsModule;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;
using Project.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    class LocationRepository : AsyncRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext db) : base(db)
        {
           
        }

        public async Task<LocationDetailDto> GetLocationDetailsAsync(int locationId, CancellationToken cancellationToken)
        {
            var location = await db.FindAsync<Location>(locationId,cancellationToken);
            if (location == null)
            {
                throw new NotFoundException($"{nameof(Location)} with {locationId} not found");
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
