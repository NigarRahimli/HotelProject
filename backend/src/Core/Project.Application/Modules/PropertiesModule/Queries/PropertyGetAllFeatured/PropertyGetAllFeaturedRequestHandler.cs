using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllFeatured;
using Project.Application.Repositories;
using Project.Infrastructure.Exceptions;
using Project.Infrastructure.Extensions;
using System;
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

        public PropertyGetAllFeaturedRequestHandler(
            IPropertyRepository propertyRepository,
            ILocationRepository locationRepository,
            IPropertyImageRepository propertyImageRepository,
            IUserRepository userRepository,
            IFacilityRepository facilityRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.propertyImageRepository = propertyImageRepository;
            this.userRepository = userRepository;
            this.facilityRepository = facilityRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<PropertyFeaturedDto>> Handle(PropertyGetAllFeaturedRequest request, CancellationToken cancellationToken)
        {
            var properties = await propertyRepository.GetPropertiesWithMostFeatured(request.Take, cancellationToken);
            if (properties == null)
            {
                throw new Exception("Featured property not found.");
            }

            var featuredDtoList = new List<PropertyFeaturedDto>();
            foreach (var property in properties)
            {
                try
                {
                    var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);
                    var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);
                    var facilitiesDetails = await facilityRepository.GetFacilitiesByPropertyIdAsync(property.Id, cancellationToken);
                    var isLiked = await userRepository.IsPropertyLikedByUserAsync(property.Id, cancellationToken);
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
                catch (NotFoundException ex)
                {
                    throw new Exception($"Failed to get location details for property with ID {property.Id}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving details for property with ID {property.Id}: {ex.Message}");
                }
            }

            return featuredDtoList;
        }
    }
}
