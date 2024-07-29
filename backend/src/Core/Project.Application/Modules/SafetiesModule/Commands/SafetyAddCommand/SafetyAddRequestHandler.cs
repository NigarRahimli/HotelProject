using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyAddCommand
{
    class SafetyAddRequestHandler : IRequestHandler<SafetyAddRequest, Safety>
    {
        private readonly ISafetyRepository safetyRepository;
        private readonly IFileService fileService;
        private readonly ILogger<SafetyAddRequestHandler> logger;

        public SafetyAddRequestHandler(ISafetyRepository safetyRepository, IFileService fileService, ILogger<SafetyAddRequestHandler> logger)
        {
            this.safetyRepository = safetyRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Safety> Handle(SafetyAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling SafetyAddRequest for Safety with Name: {SafetyName}", request.Name);

            var entity = new Safety
            {
                Name = request.Name
            };

            try
            {
                logger.LogInformation("Uploading image for Safety with Name: {SafetyName}", request.Name);
                var icon = await fileService.UploadSingleAsync(request.Image);
                entity.IconUrl = icon.Url;

                logger.LogInformation("Saving Safety entity with Name: {SafetyName}", request.Name);
                await safetyRepository.AddAsync(entity, cancellationToken);
                await safetyRepository.SaveAsync(cancellationToken);

                logger.LogInformation("Successfully added Safety with Name: {SafetyName}", request.Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding Safety with Name: {SafetyName}", request.Name);
                throw;
            }

            return entity;
        }
    }
}
