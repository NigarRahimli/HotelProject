using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetAllQuery
{
    class FacilityCountGetAllRequestHandler : IRequestHandler<FacilityCountGetAllRequest, IEnumerable<FacilityCount>>
    {
        private readonly IFacilityCountRepository facilityCountRepository;
        private readonly ILogger<FacilityCountGetAllRequestHandler> logger;

        public FacilityCountGetAllRequestHandler(IFacilityCountRepository facilityCountRepository, ILogger<FacilityCountGetAllRequestHandler> logger)
        {
            this.facilityCountRepository = facilityCountRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<FacilityCount>> Handle(FacilityCountGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityCountGetAllRequest");

            var entities = await facilityCountRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);

            logger.LogInformation("Retrieved {Count} FacilityCounts", entities.Count);

            return entities;
        }
    }
}