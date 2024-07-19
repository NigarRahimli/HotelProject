using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.KindsModule.Commands.KindRemoveCommand
{
    class KindRemoveRequestHandler : IRequestHandler<KindRemoveRequest>
    {
        private readonly IKindRepository kindRepository;
        private readonly ILogger<KindRemoveRequestHandler> logger;

        public KindRemoveRequestHandler(IKindRepository kindRepository, ILogger<KindRemoveRequestHandler> logger)
        {
            this.kindRepository = kindRepository;
            this.logger = logger;
        }

        public async Task Handle(KindRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling KindRemoveRequest for Id: {Id}", request.Id);

            var entity = await kindRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);

            if (entity == null)
            {
                logger.LogWarning("Kind with Id: {Id} not found", request.Id);

            }

            kindRepository.Remove(entity);
            await kindRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Removed Kind with Id: {Id}", request.Id);
        }
    }
}
