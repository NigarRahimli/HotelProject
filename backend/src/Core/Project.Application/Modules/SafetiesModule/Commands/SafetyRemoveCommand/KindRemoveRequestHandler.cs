using MediatR;
using Project.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyRemoveCommand
{
    class SafetyRemoveRequestHandler : IRequestHandler<SafetyRemoveRequest>
    {
        private readonly ISafetyRepository SafetyRepository;

        public SafetyRemoveRequestHandler(ISafetyRepository SafetyRepository)
        {
            this.SafetyRepository = SafetyRepository;
        }
        public async Task Handle(SafetyRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await SafetyRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            SafetyRepository.Remove(entity);
            await SafetyRepository.SaveAsync(cancellationToken);
        }
    }
}
