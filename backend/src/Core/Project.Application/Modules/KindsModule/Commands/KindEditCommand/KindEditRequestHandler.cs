using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.KindsModule.Commands.KindEditCommand
{
    class KindEditRequestHandler : IRequestHandler<KindEditRequest, Kind>
    {
        private readonly IKindRepository kindRepository;
        private readonly ILogger<KindEditRequestHandler> logger;

        public KindEditRequestHandler(IKindRepository kindRepository, ILogger<KindEditRequestHandler> logger)
        {
            this.kindRepository = kindRepository;
            this.logger = logger;
        }

        public async Task<Kind> Handle(KindEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling KindEditRequest for Id: {Id}", request.Id);

            logger.LogInformation("Retrieving kind with ID {KindId}", request.Id);
            var entity = await kindRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            logger.LogInformation("Kind with Id: {KindId} retrieved successfully", request.Id);


            entity.Name = request.Name;
            await kindRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Edited Kind with Id: {Id}, new Name: {Name}", entity.Id, entity.Name);

            return entity;
        }
    }
}
