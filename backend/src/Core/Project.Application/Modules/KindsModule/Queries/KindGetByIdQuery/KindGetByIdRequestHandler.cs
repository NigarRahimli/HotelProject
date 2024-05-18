using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.KindsModule.Queries.KindGetByIdQuery
{
    class KindGetByIdRequestHandler : IRequestHandler<KindGetByIdRequest, Kind>
    {
        private readonly IKindRepository kindRepository;

        public KindGetByIdRequestHandler(IKindRepository kindRepository)
        {
            this.kindRepository = kindRepository;
        }
        public async Task<Kind> Handle(KindGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await kindRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
