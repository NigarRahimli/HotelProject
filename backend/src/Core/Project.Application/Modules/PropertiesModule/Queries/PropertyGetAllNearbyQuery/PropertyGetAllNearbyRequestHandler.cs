using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery;
using Project.Application.Repositories;
using Project.Infrastructure.Exceptions;

public class PropertyGetAllNearbyRequestHandler : IRequestHandler<PropertyGetAllNearbyRequest, IEnumerable<PropertyWithHeartDto>>
{
    private readonly IPropertyRepository propertyRepository;
    private readonly IPropertyImageRepository propertyImageRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IUserRepository userRepository;
    private readonly ILogger<PropertyGetAllNearbyRequestHandler> logger;

    public PropertyGetAllNearbyRequestHandler(
        IPropertyRepository propertyRepository,
        ILocationRepository locationRepository,
        IPropertyImageRepository propertyImageRepository,
        IUserRepository userRepository,
        ILogger<PropertyGetAllNearbyRequestHandler> logger
    )
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
        this.propertyImageRepository = propertyImageRepository;
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public async Task<IEnumerable<PropertyWithHeartDto>> Handle(PropertyGetAllNearbyRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving nearby properties with latitude: {Latitude}, longitude: {Longitude}, take: {Take}", request.Latitude, request.Longitude, request.Take);
        var propertiesQuery = propertyRepository.GetNearbyProperties(request.Latitude, request.Longitude, 1000000000, request.Take);

        if (propertiesQuery == null)
        {
            logger.LogWarning("No nearby properties found");
            throw new Exception("Nearby property not found.");
        }

        var nearbyDtoList = new List<PropertyWithHeartDto>();

        foreach (var property in propertiesQuery)
        {
            try
            {
                logger.LogInformation("Retrieving location details for property ID {PropertyId}", property.Id);
                var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);

                logger.LogInformation("Retrieving property image details for property ID {PropertyId}", property.Id);
                var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);
                var firstPropertyImage = propertyImageDetails.FirstOrDefault();

                logger.LogInformation("Checking if property ID {PropertyId} is liked by the user", property.Id);
                var isLiked = await userRepository.IsPropertyLikedByUserAsync(property.Id, cancellationToken);

                logger.LogInformation("Retrieving user details for host ID {HostId}", property.CreatedBy);
                var user = await userRepository.GetAsync(x => x.Id == property.CreatedBy);

                var nearbyDto = new PropertyWithHeartDto
                {
                    PropertyId = property.Id,
                    Name = property.Name,
                    City = locationDetails.City,
                    Country = locationDetails.Country,
                    Address = locationDetails.Address,
                    IsLiked = isLiked,
                    Image = firstPropertyImage?.Image,
                    Url = firstPropertyImage?.Url ?? "/uploads/default/property_avatar.jpg",
                    HostId = property.CreatedBy.Value,
                    HostProfileImgUrl = user?.ProfileImgUrl
                };

                nearbyDtoList.Add(nearbyDto);
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, "Failed to get location details for property ID {PropertyId}", property.Id);
                throw new Exception($"Failed to get location details for property with ID {property.Id}: {ex.Message}");
            }
        }

        return nearbyDtoList;
    }
}
