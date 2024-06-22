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
        private readonly IMapper _mapper;
        private readonly DbSet<PropertyImage> _propertyImages;

        public PropertyImageRepository(DbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
            _propertyImages = db.Set<PropertyImage>();
        }

        public async Task<IQueryable<PropertyImageDetailsDto>> GetPropertyImageDetailsAsync(int propertyId, CancellationToken cancellationToken)
        {
            var query = _propertyImages
                .Where(pi => pi.PropertyId == propertyId && pi.DeletedBy==null)
                .Select(pi => new PropertyImageDetailsDto
                {
                    Id = pi.Id,
                    Image = pi.Image,
                    Url = pi.Url
                });

            var dto = await query.ToListAsync(cancellationToken);

            return dto.AsQueryable();
        }
    }
}
