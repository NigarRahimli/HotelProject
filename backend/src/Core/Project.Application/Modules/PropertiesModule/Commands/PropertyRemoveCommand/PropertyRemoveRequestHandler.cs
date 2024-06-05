using MediatR;
using Project.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyRemoveCommand
{
    class PropertyRemoveRequestHandler : IRequestHandler<PropertyRemoveRequest>
    {
        private readonly IPropertyRepository PropertyRepository;

        public PropertyRemoveRequestHandler(IPropertyRepository PropertyRepository)
        {
            this.PropertyRepository = PropertyRepository;
        }
        public async Task Handle(PropertyRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await PropertyRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            PropertyRepository.Remove(entity);
            await PropertyRepository.SaveAsync(cancellationToken);
        }
    }
}
