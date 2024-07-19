using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.DescriptionModule.Queries;
using Project.Application.Repositories;

namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetAllQuery
{
    class DescriptionGetAllRequestHandler : IRequestHandler<DescriptionGetAllRequest, IEnumerable<DescriptionDto>>
    {
        private readonly IDescriptionRepository descriptionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DescriptionGetAllRequestHandler> logger;

        public DescriptionGetAllRequestHandler(IDescriptionRepository descriptionRepository, IMapper mapper, ILogger<DescriptionGetAllRequestHandler> logger)
        {
            this.descriptionRepository = descriptionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<DescriptionDto>> Handle(DescriptionGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DescriptionGetAllRequest");

            var entities = await descriptionRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            if (entities == null || !entities.Any())
            {
                logger.LogWarning("No descriptions found");
                return Enumerable.Empty<DescriptionDto>();
            }

            logger.LogInformation("Mapping descriptions to DescriptionDto");
            var descDtos = mapper.Map<IEnumerable<DescriptionDto>>(entities);

            logger.LogInformation("Successfully handled DescriptionGetAllRequest");
            return descDtos;
        }
    }
}
