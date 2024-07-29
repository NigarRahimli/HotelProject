using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyEditCommand
{
    class SafetyEditRequestHandler : IRequestHandler<SafetyEditRequest, Safety>
    {
        private readonly ISafetyRepository safetyRepository;
        private readonly IFileService fileService;
        private readonly ILogger<SafetyEditRequestHandler> logger;

        public SafetyEditRequestHandler(ISafetyRepository safetyRepository, IFileService fileService, ILogger<SafetyEditRequestHandler> logger)
        {
            this.safetyRepository = safetyRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Safety> Handle(SafetyEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling SafetyEditRequest for Safety ID: {SafetyId}", request.Id);

            var entity = await safetyRepository.GetAsync(m => m.Id == request.Id && m.DeletedBy == null);

        
            logger.LogInformation("Safety with ID {SafetyId} found.", request.Id);
       

            logger.LogInformation("Updating Safety with ID: {SafetyId}. New Name: {SafetyName}", request.Id, request.Name);

            entity.Name = request.Name;

            if (request.Image != null)
            {
                try
                {
                    logger.LogInformation("Changing image for Safety with ID: {SafetyId}", request.Id);
                    var icon = await fileService.ChangeSingleFileAsync(entity.IconUrl, request.Image);
                    entity.IconUrl = icon.Url;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred while changing image for Safety with ID: {SafetyId}", request.Id);
                    throw new OperationFailedException("Error occurred while changing image for Safety");
                }
            }

            await safetyRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Successfully updated Safety with ID: {SafetyId}", request.Id);

            return entity;
        }
    }
}
