using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionAddCommand
{
    class DescriptionAddRequestHandler : IRequestHandler<DescriptionAddRequest, Description>
    {
        private readonly IDescriptionRepository DescriptionRepository;

        public DescriptionAddRequestHandler(IDescriptionRepository DescriptionRepository)
        {
            this.DescriptionRepository = DescriptionRepository;
        }
        public async Task<Description> Handle(DescriptionAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Description
            {
                Name = request.Name,
                Explanation=request.Explanation,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await DescriptionRepository.AddAsync(entity, cancellationToken);
            await DescriptionRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
