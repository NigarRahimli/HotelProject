using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Resume.Application.Services;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand
{
    class AmenityAddRequestHandler : IRequestHandler<AmenityAddRequest, Amenity>
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly IFileService fileService;


        public AmenityAddRequestHandler(IAmenityRepository amenityRepository, IFileService fileService)
        {
            this.amenityRepository = amenityRepository;
            this.fileService = fileService;
        }
        public async Task<Amenity> Handle(AmenityAddRequest request, CancellationToken cancellationToken)
        {

            var entity = new Amenity();
            entity.Name= request.Name;
            var file = await fileService.UploadSingleAsync(request.Image);
            entity.IconUrl = file.Url.ToString();
           
            await amenityRepository.AddAsync(entity, cancellationToken);
            await amenityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
