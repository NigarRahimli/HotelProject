using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetAllQuery
{
    class DescriptionGetAllRequestHandler : IRequestHandler<DescriptionGetAllRequest, IEnumerable<Description>>
    {
        private readonly IDescriptionRepository descriptionRepository;

        public DescriptionGetAllRequestHandler(IDescriptionRepository descriptionRepository)
        {
            this.descriptionRepository = descriptionRepository;
        }
        public async Task<IEnumerable<Description>> Handle(DescriptionGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await descriptionRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
