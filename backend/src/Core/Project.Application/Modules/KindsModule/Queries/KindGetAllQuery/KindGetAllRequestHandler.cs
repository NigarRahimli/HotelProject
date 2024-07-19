using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.KindsModule.Queries.KindGetAllQuery
{
    class KindGetAllRequestHandler : IRequestHandler<KindGetAllRequest, IEnumerable<KindDto>>
    {
        private readonly IKindRepository kindRepository;
        private readonly IMapper mapper;
        private readonly ILogger<KindGetAllRequestHandler> logger;

        public KindGetAllRequestHandler(IKindRepository kindRepository, IMapper mapper, ILogger<KindGetAllRequestHandler> logger)
        {
            this.kindRepository = kindRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<KindDto>> Handle(KindGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling KindGetAllRequest");

            var entities = await kindRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            var kindDtos = mapper.Map<IEnumerable<KindDto>>(entities);

            logger.LogInformation("Retrieved {Count} kinds", kindDtos.Count());
           
            return kindDtos;
        }
    }
}
