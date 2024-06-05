using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllQuery
{
    class PropertyGetAllRequestHandler : IRequestHandler<PropertyGetAllRequest, IEnumerable<Property>>
    {
        private readonly IPropertyRepository PropertyRepository;

        public PropertyGetAllRequestHandler(IPropertyRepository PropertyRepository)
        {
            this.PropertyRepository = PropertyRepository;
        }
        public async Task<IEnumerable<Property>> Handle(PropertyGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await PropertyRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
