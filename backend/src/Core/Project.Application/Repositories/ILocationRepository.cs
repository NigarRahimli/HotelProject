using Project.Application.Modules.LocationsModule;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Repositories
{
    public interface ILocationRepository : IAsyncRepository<Location>
    {
        Task<LocationDetailDto> GetLocationDetailsAsync(int locationId, CancellationToken cancellationToken);
    }
}
