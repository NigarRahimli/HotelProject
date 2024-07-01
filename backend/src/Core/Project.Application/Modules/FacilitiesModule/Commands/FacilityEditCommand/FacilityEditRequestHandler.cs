using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Resume.Application.Services;


namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityEditCommand
{
    class FacilityEditRequestHandler : IRequestHandler<FacilityEditRequest, Facility>
    {
        private readonly IFacilityRepository FacilityRepository;
        private readonly IFileService fileService;


        public FacilityEditRequestHandler(IFacilityRepository FacilityRepository, IFileService fileService)
        {
            this.FacilityRepository = FacilityRepository;
            this.fileService = fileService;
        }
        public async Task<Facility> Handle(FacilityEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await FacilityRepository.GetAsync(m=>m.Id==request.Id && m.DeletedBy==null);

            entity.Name=request.Name;
            if (request.Image is not null)
            {
                entity.IconUrl = await fileService.ChangeSingleFileAsync(entity.IconUrl, request.Image);
            }
            await FacilityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
