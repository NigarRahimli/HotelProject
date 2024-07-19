using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionAddCommand
{
    class DescriptionAddRequestHandler : IRequestHandler<DescriptionAddRequest, Description>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly ILogger<DescriptionAddRequestHandler> logger;

        public DescriptionAddRequestHandler(IDescriptionRepository descriptionRepository, ILogger<DescriptionAddRequestHandler> logger)
        {
            this.descriptionRepository = descriptionRepository;
            this.logger = logger;
        }

        public async Task<Description> Handle(DescriptionAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DescriptionAddRequest");

            var entity = new Description
            {
                Name = request.Name,
                Explanation = request.Explanation,
            };

            logger.LogInformation("Adding new Description with Name: {DescriptionName}", request.Name);
            await descriptionRepository.AddAsync(entity, cancellationToken);
            logger.LogInformation("Description added to repository");

            await descriptionRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Description saved successfully");

            return entity;
        }
    }
}
