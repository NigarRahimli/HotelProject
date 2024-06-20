using AutoMapper;
using MediatR;
using Project.Application.Modules.PropertiesModule.Commands;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Concretes;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyPagedQuery
{
    class PropertyPagedRequestHandler : IRequestHandler<PropertyPagedRequest, IPaginate<PropertyDto>>
    {
        private readonly IPropertyRepository PropertyRepository;
        private readonly IMapper mapper;

        public PropertyPagedRequestHandler(IPropertyRepository PropertyRepository, IMapper mapper)
        {
            this.PropertyRepository = PropertyRepository;
            this.mapper = mapper;
        }

        public async Task<IPaginate<PropertyDto>> Handle(PropertyPagedRequest request, CancellationToken cancellationToken)
        {

            var response = await PropertyRepository
                                .GetAll(m => m.DeletedBy == null)
                                .OrderByDescending(m => m.Id)
                                .ToPaginateAsync(request, cancellationToken); //extension methods


            var dto = mapper.Map<Paginate<PropertyDto>>(response);

            return dto;
        }
    }
}
