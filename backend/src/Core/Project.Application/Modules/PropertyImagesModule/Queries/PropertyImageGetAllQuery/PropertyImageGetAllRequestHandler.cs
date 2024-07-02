using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.PropertyImagesModule.Queries.PropertyImageGetAllQuery
{
    class PropertyImageGetAllRequestHandler : IRequestHandler<PropertyImageGetAllRequest, IEnumerable<PropertyImage>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;

        public PropertyImageGetAllRequestHandler(IPropertyImageRepository propertyImageRepository)
        {
            this.propertyImageRepository = propertyImageRepository;
        }
        public async Task<IEnumerable<PropertyImage>> Handle(PropertyImageGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await propertyImageRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
