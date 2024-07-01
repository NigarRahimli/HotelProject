using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Resume.Application.Services;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyAddCommand
{
    class SafetyAddRequestHandler : IRequestHandler<SafetyAddRequest, Safety>
    {
        private readonly ISafetyRepository safetyRepository;
        private readonly IFileService fileService;


        public SafetyAddRequestHandler(ISafetyRepository safetyRepository, IFileService fileService)
        {
            this.safetyRepository = safetyRepository;
            this.fileService = fileService;
        }
        public async Task<Safety> Handle(SafetyAddRequest request, CancellationToken cancellationToken)
        {

            var entity = new Safety();

            entity.Name = request.Name;
            var icon = await fileService.UploadSingleAsync(request.Image);
            entity.IconUrl = icon.Url;
            await safetyRepository.AddAsync(entity, cancellationToken);
            await safetyRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
