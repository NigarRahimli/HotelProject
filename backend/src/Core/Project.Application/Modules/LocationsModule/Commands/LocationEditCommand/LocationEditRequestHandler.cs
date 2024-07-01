using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.LocationsModule.Commands.LocationEditCommand
{
    class LocationEditRequestHandler : IRequestHandler<LocationEditRequest, Location>
    {
        private readonly ILocationRepository LocationRepository;

        public LocationEditRequestHandler(ILocationRepository LocationRepository)
        {
            this.LocationRepository = LocationRepository;
        }
        public async Task<Location> Handle(LocationEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await LocationRepository.GetAsync(m=>m.Id==request.Id);

            entity.Longitude= request.Longitude;
            entity.Latitude= request.Latitude;
            entity.City= request.City;
            entity.Country= request.Country;
            entity.State= request.State;
            entity.ZipCode= request.ZipCode;
            await LocationRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
