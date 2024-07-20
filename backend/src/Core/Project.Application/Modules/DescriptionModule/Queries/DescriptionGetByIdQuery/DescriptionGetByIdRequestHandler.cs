using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetByIdQuery
{
    class DescriptionGetByIdRequestHandler : IRequestHandler<DescriptionGetByIdRequest, Description>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly ILogger<DescriptionGetByIdRequestHandler> logger;

        public DescriptionGetByIdRequestHandler(IDescriptionRepository descriptionRepository, ILogger<DescriptionGetByIdRequestHandler> logger)
        {
            this.descriptionRepository = descriptionRepository;
            this.logger = logger;
        }

        public async Task<Description> Handle(DescriptionGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DescriptionGetByIdRequest for Description Id: {DescriptionId}", request.Id);

            var entity = await descriptionRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
        

            logger.LogInformation("Successfully retrieved Description with Id: {DescriptionId}", request.Id);
            return entity;
        }
    }
}
