using MediatR;
using Project.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionRemoveCommand
{
    class DescriptionRemoveRequestHandler : IRequestHandler<DescriptionRemoveRequest>
    {
        private readonly IDescriptionRepository DescriptionRepository;

        public DescriptionRemoveRequestHandler(IDescriptionRepository DescriptionRepository)
        {
            this.DescriptionRepository = DescriptionRepository;
        }
        public async Task Handle(DescriptionRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await DescriptionRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            DescriptionRepository.Remove(entity);
            await DescriptionRepository.SaveAsync(cancellationToken);
        }
    }
}
