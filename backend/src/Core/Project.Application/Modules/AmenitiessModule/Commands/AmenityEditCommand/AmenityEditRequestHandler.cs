using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand
{
    class AmenityEditRequestHandler : IRequestHandler<AmenityEditRequest, Amenity>
    {
        private readonly IAmenityRepository AmenityRepository;

        public AmenityEditRequestHandler(IAmenityRepository AmenityRepository)
        {
            this.AmenityRepository = AmenityRepository;
        }
        public async Task<Amenity> Handle(AmenityEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await AmenityRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            await AmenityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
