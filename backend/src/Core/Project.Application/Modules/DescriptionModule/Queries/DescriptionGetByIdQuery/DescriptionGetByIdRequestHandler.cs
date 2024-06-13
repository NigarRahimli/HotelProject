using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetByIdQuery
{
    class DescriptionGetByIdRequestHandler : IRequestHandler<DescriptionGetByIdRequest, Description>
    {
        private readonly IDescriptionRepository DescriptionRepository;

        public DescriptionGetByIdRequestHandler(IDescriptionRepository DescriptionRepository)
        {
            this.DescriptionRepository = DescriptionRepository;
        }
        public async Task<Description> Handle(DescriptionGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await DescriptionRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
