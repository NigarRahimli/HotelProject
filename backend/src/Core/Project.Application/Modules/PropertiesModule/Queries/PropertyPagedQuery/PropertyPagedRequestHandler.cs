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
        private readonly ILocationRepository locationRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;

        public PropertyPagedRequestHandler(IPropertyRepository propertyRepository, IMapper mapper, IReservationRepository reservationRepository, ILocationRepository locationRepository)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
            this.reservationRepository = reservationRepository;
            this.locationRepository = locationRepository;
        }

        public async Task<IPaginate<PropertyDto>> Handle(PropertyPagedRequest request, CancellationToken cancellationToken)
        {
           
            var query = propertyRepository.GetAll(m => m.DeletedBy == null);

            
            if (request.KindId.HasValue)
            {
                query = query.Where(m => m.KindId == request.KindId.Value);
            }

            IQueryable<int> locationIdsQuery = null;
            if (!string.IsNullOrEmpty(request.CityName))
            {
                locationIdsQuery = locationRepository
                    .GetAll(l => l.City == request.CityName)
                    .Select(l => l.Id);
            }
            if (locationIdsQuery != null)
            {
                query = query.Where(p => locationIdsQuery.Contains(p.LocationId));
            }

         

            if (request.CheckInTime.HasValue && request.CheckOutTime.HasValue)
            {
                var overlappingReservations = await reservationRepository.GetOverlappingReservationsAsync(request.CheckInTime.Value, request.CheckOutTime.Value);
                var overlappingPropertyIds = overlappingReservations.Select(r => r.PropertyId);

                query = query.Where(m => !overlappingPropertyIds.Contains(m.Id));
            }

            if (request.GuestNum.HasValue)
            {
                query = query.Where(m => m.GuestNum >= request.GuestNum.Value);
            }



            var paginatedProperties = await query.OrderByDescending(m => m.Id).ToPaginateAsync(request, cancellationToken);

          
            var dto = mapper.Map<Paginate<PropertyDto>>(paginatedProperties);

            return dto;
        }
    }

}
