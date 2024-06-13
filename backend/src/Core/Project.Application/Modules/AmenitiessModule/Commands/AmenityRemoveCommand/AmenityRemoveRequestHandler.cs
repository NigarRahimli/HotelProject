using MediatR;
using Project.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityRemoveCommand
{
    class AmenityRemoveRequestHandler : IRequestHandler<AmenityRemoveRequest>
    {
        private readonly IAmenityRepository AmenityRepository;

        public AmenityRemoveRequestHandler(IAmenityRepository AmenityRepository)
        {
            this.AmenityRepository = AmenityRepository;
        }
        public async Task Handle(AmenityRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await AmenityRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            AmenityRepository.Remove(entity);
            await AmenityRepository.SaveAsync(cancellationToken);
        }
    }
}
