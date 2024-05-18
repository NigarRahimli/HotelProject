using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetAllQuery
{
    class DescriptionGetAllRequestHandler : IRequestHandler<DescriptionGetAllRequest, IEnumerable<Description>>
    {
        private readonly IDescriptionRepository DescriptionRepository;

        public DescriptionGetAllRequestHandler(IDescriptionRepository DescriptionRepository)
        {
            this.DescriptionRepository = DescriptionRepository;
        }
        public async Task<IEnumerable<Description>> Handle(DescriptionGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await DescriptionRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
