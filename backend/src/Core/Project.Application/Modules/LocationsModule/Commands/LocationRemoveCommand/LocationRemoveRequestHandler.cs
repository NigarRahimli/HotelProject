using MediatR;
using Project.Application.Repositories;


namespace Project.Application.Modules.LocationsModule.Commands.LocationRemoveCommand
{
    class LocationRemoveRequestHandler : IRequestHandler<LocationRemoveRequest>
    {
        private readonly ILocationRepository LocationRepository;

        public LocationRemoveRequestHandler(ILocationRepository LocationRepository)
        {
            this.LocationRepository = LocationRepository;
        }
        public async Task Handle(LocationRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await LocationRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            LocationRepository.Remove(entity);
            await LocationRepository.SaveAsync(cancellationToken);
        }
    }
}
