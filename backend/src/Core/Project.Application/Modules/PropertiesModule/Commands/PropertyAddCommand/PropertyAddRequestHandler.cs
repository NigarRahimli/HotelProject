using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyAddCommand
{
    class PropertyAddRequestHandler : IRequestHandler<PropertyAddRequest, Property>
    {
        private readonly IPropertyRepository PropertyRepository;

        public PropertyAddRequestHandler(IPropertyRepository PropertyRepository)
        {
            this.PropertyRepository = PropertyRepository;
        }
        public async Task<Property> Handle(PropertyAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Property
            {
                Name = request.Name,
                DescriptionId = request.DescriptionId,
                KindId= request.KindId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await PropertyRepository.AddAsync(entity, cancellationToken);
            await PropertyRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
