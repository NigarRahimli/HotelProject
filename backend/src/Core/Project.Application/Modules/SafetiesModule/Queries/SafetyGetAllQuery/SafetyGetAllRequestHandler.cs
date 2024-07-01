using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.SafetiesModule.Queries.SafetyGetAllQuery
{
    class SafetyGetAllRequestHandler : IRequestHandler<SafetyGetAllRequest, IEnumerable<Safety>>
    {
        private readonly ISafetyRepository SafetyRepository;

        public SafetyGetAllRequestHandler(ISafetyRepository SafetyRepository)
        {
            this.SafetyRepository = SafetyRepository;
        }
        public async Task<IEnumerable<Safety>> Handle(SafetyGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await SafetyRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
