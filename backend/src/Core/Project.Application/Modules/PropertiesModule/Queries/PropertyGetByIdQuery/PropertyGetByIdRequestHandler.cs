using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetByIdQuery
{
    class PropertyGetByIdRequestHandler : IRequestHandler<PropertyGetByIdRequest, Property>
    {
        private readonly IPropertyRepository PropertyRepository;

        public PropertyGetByIdRequestHandler(IPropertyRepository PropertyRepository)
        {
            this.PropertyRepository = PropertyRepository;
        }
        public async Task<Property> Handle(PropertyGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await PropertyRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
