using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityAddCommand
{
    class FacilityAddRequestHandler : IRequestHandler<FacilityAddRequest, Facility>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly IFileService fileService;

        public FacilityAddRequestHandler(IFacilityRepository facilityRepository, IFileService fileService)
        {
            this.facilityRepository = facilityRepository;
            this.fileService = fileService;
        }
        public async Task<Facility> Handle(FacilityAddRequest request, CancellationToken cancellationToken)
        {

            var entity = new Facility();

            entity.Name = request.Name;
            var icon = await fileService.UploadSingleAsync(request.Image,"icons");
            entity.IconUrl=icon.Url;

      
            await facilityRepository.AddAsync(entity, cancellationToken);
            await facilityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
