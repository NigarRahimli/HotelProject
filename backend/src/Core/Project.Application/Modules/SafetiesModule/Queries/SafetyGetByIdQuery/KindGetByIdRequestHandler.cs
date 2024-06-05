using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.SafetiesModule.Queries.SafetyGetByIdQuery
{
    class SafetyGetByIdRequestHandler : IRequestHandler<SafetyGetByIdRequest, Safety>
    {
        private readonly ISafetyRepository SafetyRepository;

        public SafetyGetByIdRequestHandler(ISafetyRepository SafetyRepository)
        {
            this.SafetyRepository = SafetyRepository;
        }
        public async Task<Safety> Handle(SafetyGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await SafetyRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
