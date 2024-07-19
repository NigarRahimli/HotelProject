using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.KindsModule.Commands.KindAddCommand
{
    class KindAddRequestHandler : IRequestHandler<KindAddRequest, Kind>
    {
        private readonly IKindRepository kindRepository;
        private readonly ILogger<KindAddRequestHandler> logger;

        public KindAddRequestHandler(IKindRepository kindRepository, ILogger<KindAddRequestHandler> logger)
        {
            this.kindRepository = kindRepository;
            this.logger = logger;
        }

        public async Task<Kind> Handle(KindAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling KindAddRequest with Name: {Name}", request.Name);

            var entity = new Kind
            {
                Name = request.Name,
            };

            await kindRepository.AddAsync(entity, cancellationToken);
            await kindRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Added new Kind with Id: {Id} and Name: {Name}", entity.Id, entity.Name);

            return entity;
        }
    }
}
