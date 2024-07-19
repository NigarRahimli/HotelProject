using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionEditCommand
{
    class DescriptionEditRequestHandler : IRequestHandler<DescriptionEditRequest, Description>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly ILogger<DescriptionEditRequestHandler> logger;

        public DescriptionEditRequestHandler(IDescriptionRepository descriptionRepository, ILogger<DescriptionEditRequestHandler> logger)
        {
            this.descriptionRepository = descriptionRepository;
            this.logger = logger;
        }

        public async Task<Description> Handle(DescriptionEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DescriptionEditRequest for Description Id: {DescriptionId}", request.Id);

            var entity = await descriptionRepository.GetAsync(m => m.Id == request.Id && m.DeletedBy == null,cancellationToken);
            if (entity == null)
            {
                logger.LogWarning("Description with Id: {DescriptionId} not found", request.Id);
            }

            logger.LogInformation("Editing Description with Id: {DescriptionId}", request.Id);
            entity.Name = request.Name;
            entity.Explanation = request.Explanation;

            await descriptionRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Description with Id: {DescriptionId} updated successfully", request.Id);

            return entity;
        }
    }
}
