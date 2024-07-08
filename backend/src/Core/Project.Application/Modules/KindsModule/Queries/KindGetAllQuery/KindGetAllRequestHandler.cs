using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.KindsModule.Queries.KindGetAllQuery
{
    class KindGetAllRequestHandler : IRequestHandler<KindGetAllRequest, IEnumerable<KindDto>>
    {
        private readonly IKindRepository kindRepository;
        private readonly IMapper mapper;

        public KindGetAllRequestHandler(IKindRepository kindRepository, IMapper mapper)
        {
            this.kindRepository = kindRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<KindDto>> Handle(KindGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await kindRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            var kindDtos = mapper.Map<IEnumerable<KindDto>>(entities);
            return kindDtos;
        }
    }
}
