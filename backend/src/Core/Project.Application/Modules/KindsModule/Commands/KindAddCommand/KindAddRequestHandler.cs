using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.KindsModule.Commands.KindAddCommand
{
    class KindAddRequestHandler : IRequestHandler<KindAddRequest, Kind>
    {
        private readonly IKindRepository kindRepository;

        public KindAddRequestHandler(IKindRepository kindRepository)
        {
            this.kindRepository = kindRepository;
        }
        public async Task<Kind> Handle(KindAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Kind
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await kindRepository.AddAsync(entity, cancellationToken);
            await kindRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
