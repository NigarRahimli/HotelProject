using MediatR;
using Project.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.KindsModule.Commands.KindRemoveCommand
{
    class KindRemoveRequestHandler : IRequestHandler<KindRemoveRequest>
    {
        private readonly IKindRepository kindRepository;

        public KindRemoveRequestHandler(IKindRepository kindRepository)
        {
            this.kindRepository = kindRepository;
        }
        public async Task Handle(KindRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await kindRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            kindRepository.Remove(entity);
            await kindRepository.SaveAsync(cancellationToken);
        }
    }
}
