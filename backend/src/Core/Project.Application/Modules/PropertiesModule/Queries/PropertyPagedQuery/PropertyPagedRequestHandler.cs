using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.PropertiesModule.Commands;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Concretes;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyPagedQuery
{
    public class PropertyPagedRequestHandler : IRequestHandler<PropertyPagedRequest, IPaginate<PropertyDto>>
    {
        private readonly IPropertyRepository PropertyRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;

        public PropertyPagedRequestHandler(IPropertyRepository propertyRepository, IMapper mapper, IReservationRepository reservationRepository)
        {
            PropertyRepository = propertyRepository;
            this.mapper = mapper;
            this.reservationRepository = reservationRepository;
        }

        public async Task<IPaginate<PropertyDto>> Handle(PropertyPagedRequest request, CancellationToken cancellationToken)
        {
            var paginatedProperties = await PropertyRepository
                    .GetAll(m => m.DeletedBy == null).OrderByDescending(m => m.Id).ToPaginateAsync(request, cancellationToken); ;
         
            if (request.CheckInTime.HasValue && request.CheckOutTime.HasValue)
            {
              
                var overlappingReservations = await reservationRepository.GetOverlappingReservationsAsync(request.CheckInTime.Value, request.CheckOutTime.Value);

                var overlappingPropertyIds = overlappingReservations.Select(r => r.PropertyId);

                 paginatedProperties = await PropertyRepository
                    .GetAll(m => m.DeletedBy == null && !overlappingPropertyIds.Contains(m.Id))
                    .OrderByDescending(m => m.Id)
                    .ToPaginateAsync(request, cancellationToken);
            }
            

            var dto = mapper.Map<Paginate<PropertyDto>>(paginatedProperties);

            return dto;
        }
    }
    }
