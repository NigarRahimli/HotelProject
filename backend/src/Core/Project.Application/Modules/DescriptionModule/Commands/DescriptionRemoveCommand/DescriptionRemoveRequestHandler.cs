using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionRemoveCommand
{
    class DescriptionRemoveRequestHandler : IRequestHandler<DescriptionRemoveRequest>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly ILogger<DescriptionRemoveRequestHandler> logger;

        public DescriptionRemoveRequestHandler(IDescriptionRepository descriptionRepository, ILogger<DescriptionRemoveRequestHandler> logger)
        {
            this.descriptionRepository = descriptionRepository;
            this.logger = logger;
        }

        public async Task Handle(DescriptionRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DescriptionRemoveRequest for Description Id: {DescriptionId}", request.Id);

            logger.LogInformation("Retrieving Description with ID {Id}", request.Id);
            var entity = await descriptionRepository.GetAsync(x => x.Id == request.Id, cancellationToken);
            logger.LogInformation("Description with Id: {Id} retrieved successfully", request.Id);


            logger.LogInformation("Removing Description with Id: {DescriptionId}", request.Id);
            descriptionRepository.Remove(entity);

            await descriptionRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Description with Id: {DescriptionId} removed successfully", request.Id);
        }
    }
}
