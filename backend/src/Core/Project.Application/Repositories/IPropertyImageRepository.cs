using Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;


namespace Project.Application.Repositories
{
    public interface IPropertyImageRepository: IAsyncRepository<PropertyImage>
    {
        Task<IQueryable<PropertyImageDetailsDto>> GetPropertyImageDetailsAsync(int propertyId, CancellationToken cancellationToken);

    }
}
