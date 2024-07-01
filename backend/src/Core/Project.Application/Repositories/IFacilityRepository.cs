using Project.Application.Modules.FacilitiesModule.Queries;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Repositories
{
    public interface IFacilityRepository:IAsyncRepository<Facility>
    {
        Task<IEnumerable<FacilityDetailDto>> GetFacilitiesByPropertyIdAsync(int propertyId,CancellationToken cancellationToken);
    }
}
