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
        private readonly IPropertyRepository propertyRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;

        public PropertyPagedRequestHandler(IPropertyRepository propertyRepository, IMapper mapper, IReservationRepository reservationRepository)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
            this.reservationRepository = reservationRepository;
        }

        public async Task<IPaginate<PropertyDto>> Handle(PropertyPagedRequest request, CancellationToken cancellationToken)
        {
           
            var query = propertyRepository.GetAll(m => m.DeletedBy == null);

            
            if (request.KindId.HasValue)
            {
                query = query.Where(m => m.KindId == request.KindId.Value);
            }

            if (request.GuestNum.HasValue)
            {
                query = query.Where(m => m.GuestNum >= request.GuestNum.Value);
            }

            if (request.CheckInTime.HasValue && request.CheckOutTime.HasValue)
            {
                var overlappingReservations = await reservationRepository.GetOverlappingReservationsAsync(request.CheckInTime.Value, request.CheckOutTime.Value);
                var overlappingPropertyIds = overlappingReservations.Select(r => r.PropertyId);

                query = query.Where(m => !overlappingPropertyIds.Contains(m.Id));
            }

        

            var paginatedProperties = await query.OrderByDescending(m => m.Id).ToPaginateAsync(request, cancellationToken);

          
            var dto = mapper.Map<Paginate<PropertyDto>>(paginatedProperties);

            return dto;
        }
    }

}
