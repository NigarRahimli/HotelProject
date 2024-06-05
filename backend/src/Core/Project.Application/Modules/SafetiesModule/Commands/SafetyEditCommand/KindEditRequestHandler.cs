using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyEditCommand
{
    class SafetyEditRequestHandler : IRequestHandler<SafetyEditRequest, Safety>
    {
        private readonly ISafetyRepository SafetyRepository;

        public SafetyEditRequestHandler(ISafetyRepository SafetyRepository)
        {
            this.SafetyRepository = SafetyRepository;
        }
        public async Task<Safety> Handle(SafetyEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await SafetyRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            await SafetyRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
