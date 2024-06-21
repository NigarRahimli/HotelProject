using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllQuery
{
    class PropertyGetAllRequestHandler : IRequestHandler<PropertyGetAllRequest, IEnumerable<Property>>
    {
        private readonly IPropertyRepository propertyRepository;

        public PropertyGetAllRequestHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }
        public async Task<IEnumerable<Property>> Handle(PropertyGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await propertyRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
