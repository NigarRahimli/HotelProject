using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyAddCommand
{
    class SafetyAddRequestHandler : IRequestHandler<SafetyAddRequest, Safety>
    {
        private readonly ISafetyRepository SafetyRepository;

        public SafetyAddRequestHandler(ISafetyRepository SafetyRepository)
        {
            this.SafetyRepository = SafetyRepository;
        }
        public async Task<Safety> Handle(SafetyAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Safety
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await SafetyRepository.AddAsync(entity, cancellationToken);
            await SafetyRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
