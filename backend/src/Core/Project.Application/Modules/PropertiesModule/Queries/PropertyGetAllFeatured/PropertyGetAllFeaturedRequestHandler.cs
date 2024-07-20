using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllFeatured;
using Project.Application.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Handlers
{
    public class PropertyGetAllFeaturedRequestHandler : IRequestHandler<PropertyGetAllFeaturedRequest, IEnumerable<PropertyFeaturedDto>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IUserRepository userRepository;
        private readonly IFacilityRepository facilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<PropertyGetAllFeaturedRequestHandler> logger;

        public PropertyGetAllFeaturedRequestHandler(
            IPropertyRepository propertyRepository,
            ILocationRepository locationRepository,
            IPropertyImageRepository propertyImageRepository,
            IUserRepository userRepository,
            IFacilityRepository facilityRepository,
            IHttpContextAccessor httpContextAccessor,
            ILogger<PropertyGetAllFeaturedRequestHandler> logger)
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.propertyImageRepository = propertyImageRepository;
            this.userRepository = userRepository;
            this.facilityRepository = facilityRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<IEnumerable<PropertyFeaturedDto>> Handle(PropertyGetAllFeaturedRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving properties with most featured count, take: {Take}", request.Take);
            var properties = await propertyRepository.GetPropertiesWithMostFeatured(request.Take, cancellationToken);

            if (properties == null)
            {
                logger.LogWarning("No featured properties found.");
                return new List<PropertyFeaturedDto>();
            }

            var featuredDtoList = new List<PropertyFeaturedDto>();

            foreach (var property in properties)
            {
                logger.LogInformation("Processing property ID: {PropertyId}", property.Id);

                logger.LogInformation("Retrieving location details for property ID: {PropertyId}", property.Id);
                var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);

                logger.LogInformation("Retrieving property image details for property ID: {PropertyId}", property.Id);
                var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);

                logger.LogInformation("Retrieving facilities for property ID: {PropertyId}", property.Id);
                var facilitiesDetails = await facilityRepository.GetFacilitiesByPropertyIdAsync(property.Id, cancellationToken);

                logger.LogInformation("Checking if property ID: {PropertyId} is liked by the user", property.Id);
                var isLiked = await userRepository.IsPropertyLikedByUserAsync(property.Id, cancellationToken);

                logger.LogInformation("Retrieving user details for host ID: {HostId}", property.CreatedBy);
                var user = await userRepository.GetAsync(x => x.Id == property.CreatedBy);

                var featuredDto = new PropertyFeaturedDto
                {
                    PropertyId = property.Id,
                    Name = property.Name,
                    City = locationDetails.City,
                    Country = locationDetails.Country,
                    Address = locationDetails.Address,
                    IsLiked = isLiked,
                    HostId = property.CreatedBy.Value,
                    HostProfileImgUrl = user?.ProfileImgUrl,
                    PropertyImageDetails = propertyImageDetails,
                    FacilitiesDetails = facilitiesDetails,
                    MinPrice = property.MinPrice,
                    MaxPrice = property.LongPrice,
                };

                featuredDtoList.Add(featuredDto);
            }

            logger.LogInformation("Successfully retrieved and processed featured properties.");

            return featuredDtoList;
        }
    }
}
