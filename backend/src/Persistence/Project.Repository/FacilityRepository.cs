using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.FacilitiesModule.Queries;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Repository
{
     class FacilityRepository : AsyncRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(DbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<FacilityDetailDto>> GetFacilitiesByPropertyIdAsync(int propertyId,CancellationToken cancellationToken)
        {
            var facilityDtos = await (from fc in db.Set<FacilityCount>()
                                      join f in db.Set<Facility>() on fc.FacilityId equals f.Id
                                      where fc.PropertyId == propertyId && fc.DeletedBy==null &&f.DeletedBy==null
                                      select new FacilityDetailDto
                                      {
                                          Id = fc.Id,
                                          Name = f.Name,
                                          IconUrl = f.IconUrl,
                                          Count = fc.Count
                                      }).ToListAsync(cancellationToken);

            return facilityDtos;
        }
    }
}
