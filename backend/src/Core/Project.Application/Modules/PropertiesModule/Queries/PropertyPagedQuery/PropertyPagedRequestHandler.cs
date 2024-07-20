using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllFeatured;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Concretes;
using Project.Infrastructure.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyPagedQuery
{
    public class PropertyPagedRequestHandler : IRequestHandler<PropertyPagedRequest, IPaginate<PropertyFilteredDto>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IUserRepository userRepository;
        private readonly IFacilityRepository facilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PropertyPagedRequestHandler> logger;

        public PropertyPagedRequestHandler(
            IPropertyRepository propertyRepository,
            IMapper mapper,
            IReservationRepository reservationRepository,
            ILocationRepository locationRepository,
            IPropertyImageRepository propertyImageRepository,
            IUserRepository userRepository,
            IFacilityRepository facilityRepository,
            IHttpContextAccessor httpContextAccessor,
            ILogger<PropertyPagedRequestHandler> logger)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
            this.reservationRepository = reservationRepository;
            this.locationRepository = locationRepository;
            this.propertyImageRepository = propertyImageRepository;
            this.userRepository = userRepository;
            this.facilityRepository = facilityRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<IPaginate<PropertyFilteredDto>> Handle(PropertyPagedRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling PropertyPagedRequest");

            var query = propertyRepository.GetAll(m => m.DeletedBy == null);

            if (request.KindId.HasValue)
            {
                logger.LogInformation("Filtering properties by KindId: {KindId}", request.KindId.Value);
                query = query.Where(m => m.KindId == request.KindId.Value);
            }

            IQueryable<int> locationIdsQuery = null;
            if (!string.IsNullOrEmpty(request.CityName))
            {
                logger.LogInformation("Filtering properties by CityName: {CityName}", request.CityName);
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
                logger.LogInformation("Filtering properties by CheckInTime: {CheckInTime} and CheckOutTime: {CheckOutTime}", request.CheckInTime.Value, request.CheckOutTime.Value);
                var overlappingReservations = await reservationRepository.GetOverlappingReservationsAsync(request.CheckInTime.Value, request.CheckOutTime.Value);
                var overlappingPropertyIds = overlappingReservations.Select(r => r.PropertyId);

                query = query.Where(m => !overlappingPropertyIds.Contains(m.Id));
            }

            if (request.GuestNum.HasValue)
            {
                logger.LogInformation("Filtering properties by GuestNum: {GuestNum}", request.GuestNum.Value);
                query = query.Where(m => m.GuestNum >= request.GuestNum.Value);
            }

            logger.LogInformation("Paginating properties");
            var paginatedProperties = await query.OrderByDescending(m => m.Id).ToPaginateAsync(request, cancellationToken);

            logger.LogInformation("Mapping paginated properties to DTOs");
            var paginatedDto = mapper.Map<Paginate<PropertyFilteredDto>>(paginatedProperties);

            var propertyFilteredDtos = new List<PropertyFilteredDto>();
            foreach (var property in paginatedProperties.Items)
            {
                logger.LogInformation("Fetching additional details for property Id: {PropertyId}", property.Id);
                var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);
                var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);
                var facilitiesDetails = await facilityRepository.GetFacilitiesByPropertyIdAsync(property.Id, cancellationToken);
                var isLiked = await userRepository.IsPropertyLikedByUserAsync(property.Id, cancellationToken);

                var propertyDto = new PropertyFilteredDto
                {
                    PropertyId = property.Id,
                    Name = property.Name,
                    City = locationDetails.City,
                    Country = locationDetails.Country,
                    Address = locationDetails.Address,
                    IsLiked = isLiked,
                    PropertyImageDetails = propertyImageDetails,
                    MinPrice = property.MinPrice,
                    MaxPrice = property.LongPrice,
                };

                propertyFilteredDtos.Add(propertyDto);
            }

            paginatedDto.Items = propertyFilteredDtos;

            logger.LogInformation("PropertyPagedRequest handled successfully");

            return paginatedDto;
        }
    }
}
