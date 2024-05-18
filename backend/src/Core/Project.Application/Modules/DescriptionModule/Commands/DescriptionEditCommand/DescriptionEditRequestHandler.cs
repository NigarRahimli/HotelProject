using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionEditCommand
{
    class DescriptionEditRequestHandler : IRequestHandler<DescriptionEditRequest, Description>
    {
        private readonly IDescriptionRepository DescriptionRepository;

        public DescriptionEditRequestHandler(IDescriptionRepository DescriptionRepository)
        {
            this.DescriptionRepository = DescriptionRepository;
        }
        public async Task<Description> Handle(DescriptionEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await DescriptionRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            entity.Explanation=request.Explanation;
            await DescriptionRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
