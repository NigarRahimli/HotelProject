using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetByIdQuery
{
    class FacilityCountGetByIdRequestHandler : IRequestHandler<FacilityCountGetByIdRequest, FacilityCount>
    {
        private readonly IFacilityCountRepository facilityCountRepository;
        private readonly ILogger<FacilityCountGetByIdRequestHandler> logger;

        public FacilityCountGetByIdRequestHandler(IFacilityCountRepository facilityCountRepository, ILogger<FacilityCountGetByIdRequestHandler> logger)
        {
            this.facilityCountRepository = facilityCountRepository;
            this.logger = logger;
        }

        public async Task<FacilityCount> Handle(FacilityCountGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityCountGetByIdRequest for Id: {Id}", request.Id);

            var entity = await facilityCountRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);

            logger.LogInformation("Retrieved FacilityCount with Id: {Id}", request.Id);

            return entity;
        }
    }
}
