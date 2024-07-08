using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Repository
{
    public class PropertyImageRepository : AsyncRepository<PropertyImage>, IPropertyImageRepository
    {


        public PropertyImageRepository(DbContext db, IMapper mapper) : base(db)
        {
         
           
        }

        public async Task<IQueryable<PropertyImageDetailsDto>> GetPropertyImageDetailsAsync(int propertyId, CancellationToken cancellationToken)
        {
            var query = db.Set<PropertyImage>()
                .Where(pi => pi.PropertyId == propertyId && pi.DeletedBy==null)
                .Select(pi => new PropertyImageDetailsDto
                {
                    Id = pi.Id,
                    Image = pi.Image,
                    Url = pi.Url,
                });

            var dto = await query.ToListAsync(cancellationToken);

            return dto.AsQueryable();
        }
    }
}
