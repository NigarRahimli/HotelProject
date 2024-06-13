using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.KindsModule.Queries.KindGetAllQuery
{
    class KindGetAllRequestHandler : IRequestHandler<KindGetAllRequest, IEnumerable<Kind>>
    {
        private readonly IKindRepository kindRepository;

        public KindGetAllRequestHandler(IKindRepository kindRepository)
        {
            this.kindRepository = kindRepository;
        }
        public async Task<IEnumerable<Kind>> Handle(KindGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await kindRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
