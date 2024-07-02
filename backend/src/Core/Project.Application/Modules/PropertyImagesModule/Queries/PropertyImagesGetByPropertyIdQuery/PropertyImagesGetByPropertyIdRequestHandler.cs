using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.PropertiesModule.Commands;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery
{
    internal class PropertyImagesGetByPropertyIdRequestHandler : IRequestHandler<PropertyImagesGetByPropertyIdRequest, IEnumerable<PropertyImageDetailsDto>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IMapper mapper;

        public PropertyImagesGetByPropertyIdRequestHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PropertyImageDetailsDto>> Handle(PropertyImagesGetByPropertyIdRequest request, CancellationToken cancellationToken)
        {
            var entities = await propertyImageRepository.GetAll(m => m.DeletedBy == null && m.PropertyId==request.PropertyId).ToListAsync(cancellationToken);
            var dto = mapper.Map<IEnumerable<PropertyImageDetailsDto>>(entities);
            return dto;
        }
    }
}

