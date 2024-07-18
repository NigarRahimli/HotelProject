using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;


namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand
{
    class AmenityAddRequestHandler : IRequestHandler<AmenityAddRequest, Amenity>
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly IFileService fileService;
        private readonly ILogger<AmenityAddRequestHandler> logger;

        public AmenityAddRequestHandler(IAmenityRepository amenityRepository, IFileService fileService,ILogger<AmenityAddRequestHandler> logger)
        {
            this.amenityRepository = amenityRepository;
            this.fileService = fileService;
            this.logger = logger;
        }
        public async Task<Amenity> Handle(AmenityAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting... Message: {@Name},{Surname}", "Demo", "SurDemo");
            var entity = new Amenity();
            entity.Name= request.Name;
            var file = await fileService.UploadSingleAsync(request.Image);
            entity.IconUrl = file.Url.ToString();
           
            await amenityRepository.AddAsync(entity, cancellationToken);
            await amenityRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Finished... Message: {@Name},{Surname}", "Demo", "SurDemo");

            return entity;
        }
    }
}
