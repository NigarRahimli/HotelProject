using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;


namespace Project.Application.Modules.SafetiesModule.Commands.SafetyEditCommand
{
    class SafetyEditRequestHandler : IRequestHandler<SafetyEditRequest, Safety>
    {
        private readonly ISafetyRepository safetyRepository;
        private readonly IFileService fileService;

        public SafetyEditRequestHandler(ISafetyRepository safetyRepository, IFileService fileService)
        {
            this.safetyRepository = safetyRepository;
            this.fileService = fileService;
        }
        public async Task<Safety> Handle(SafetyEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await safetyRepository.GetAsync(m=>m.Id==request.Id && m.DeletedBy==null);

            entity.Name=request.Name;
            if (request.Image is not null)
            {
                var icon= await fileService.ChangeSingleFileAsync(entity.IconUrl, request.Image);
                entity.IconUrl = icon.Url;
            }
            await safetyRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
