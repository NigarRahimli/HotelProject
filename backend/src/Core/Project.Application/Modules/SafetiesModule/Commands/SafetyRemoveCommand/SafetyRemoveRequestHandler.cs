using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyRemoveCommand
{
    class SafetyRemoveRequestHandler : IRequestHandler<SafetyRemoveRequest>
    {
        private readonly ISafetyRepository safetyRepository;
        private readonly ILogger<SafetyRemoveRequestHandler> logger;

        public SafetyRemoveRequestHandler(ISafetyRepository safetyRepository, ILogger<SafetyRemoveRequestHandler> logger)
        {
            this.safetyRepository = safetyRepository;
            this.logger = logger;
        }

        public async Task Handle(SafetyRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling SafetyRemoveRequest for Safety ID: {SafetyId}", request.Id);

            var entity = await safetyRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);


            logger.LogInformation("Removing Safety with ID: {SafetyId}", request.Id);

            safetyRepository.Remove(entity);

            await safetyRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Successfully removed Safety with ID: {SafetyId}", request.Id);
        }
    }
}
