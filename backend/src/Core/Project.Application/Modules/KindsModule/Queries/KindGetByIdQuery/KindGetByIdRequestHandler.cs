using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.KindsModule.Queries.KindGetByIdQuery
{
    class KindGetByIdRequestHandler : IRequestHandler<KindGetByIdRequest, Kind>
    {
        private readonly IKindRepository kindRepository;
        private readonly ILogger<KindGetByIdRequestHandler> logger;

        public KindGetByIdRequestHandler(IKindRepository kindRepository, ILogger<KindGetByIdRequestHandler> logger)
        {
            this.kindRepository = kindRepository;
            this.logger = logger;
        }

        public async Task<Kind> Handle(KindGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling KindGetByIdRequest for KindId: {KindId}", request.Id);

            var entity = await kindRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            if (entity == null)
            {
                logger.LogWarning("Kind with Id: {KindId} not found", request.Id);
            }
            else
            {
                logger.LogInformation("Kind with Id: {KindId} retrieved successfully", request.Id);
            }

            return entity;
        }
    }
}
