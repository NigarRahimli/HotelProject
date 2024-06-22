using MediatR;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery;
using Project.Application.Repositories;
using Project.Infrastructure.Exceptions;

public class PropertyGetAllNearbyRequestHandler : IRequestHandler<PropertyGetAllNearbyRequest, IEnumerable<PropertyWithHeartDto>>
{
    private readonly IPropertyRepository propertyRepository;
    private readonly IPropertyImageRepository propertyImageRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IFUserRepository userRepository;

    public PropertyGetAllNearbyRequestHandler(IPropertyRepository propertyRepository, ILocationRepository locationRepository, IPropertyImageRepository propertyImageRepository, IFUserRepository userRepository)
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
        this.propertyImageRepository = propertyImageRepository;
        this.userRepository = userRepository;
    }

    public async Task<IEnumerable<PropertyWithHeartDto>> Handle(PropertyGetAllNearbyRequest request, CancellationToken cancellationToken)
    {
     
        var propertiesQuery = propertyRepository.GetNearbyProperties(request.Latitude, request.Longitude, 1000000, request.Number);

        
        if (propertiesQuery == null)
        {
            throw new Exception("Nearby property not found.");
        }

        var nearbyDtoList = new List<PropertyWithHeartDto>();

        foreach (var property in propertiesQuery)
        {
            try
            {
                var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);

                var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);
                var firstPropertyImage = propertyImageDetails.FirstOrDefault();
                var isLiked = await userRepository.IsPropertyLikedByUserAsync(request.UserId, property.Id, cancellationToken);

                var nearbyDto = new PropertyWithHeartDto
                {
                    PropertyId = property.Id,
                    Name = property.Name,
                    City = locationDetails.City,
                    Country = locationDetails.Country,
                    Address = locationDetails.Address,
                    IsLiked=isLiked,
                    Image = firstPropertyImage?.Image,
                    Url = firstPropertyImage?.Url
                };

                nearbyDtoList.Add(nearbyDto);
            }
            catch (NotFoundException ex)
            {
                throw new Exception($"Failed to get location details for property with ID {property.Id}: {ex.Message}");
            }
        }

        return nearbyDtoList;
    }
}
