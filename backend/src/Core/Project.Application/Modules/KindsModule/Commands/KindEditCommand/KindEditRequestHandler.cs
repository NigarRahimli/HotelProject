using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.KindsModule.Commands.KindEditCommand
{
    class KindEditRequestHandler : IRequestHandler<KindEditRequest, Kind>
    {
        private readonly IKindRepository kindRepository;

        public KindEditRequestHandler(IKindRepository kindRepository)
        {
            this.kindRepository = kindRepository;
        }
        public async Task<Kind> Handle(KindEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await kindRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            await kindRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
