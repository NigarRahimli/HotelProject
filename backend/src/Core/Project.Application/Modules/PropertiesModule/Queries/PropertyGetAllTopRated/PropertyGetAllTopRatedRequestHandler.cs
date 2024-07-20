using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllTopRated;
using Project.Application.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class PropertyGetAllTopRatedRequestHandler : IRequestHandler<PropertyGetAllTopRatedRequest, IEnumerable<PropertyWithHeartAndRateDto>>
{
    private readonly IPropertyRepository propertyRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IUserRepository userRepository;
    private readonly IPropertyImageRepository propertyImageRepository;
    private readonly ILogger<PropertyGetAllTopRatedRequestHandler> logger;

    public PropertyGetAllTopRatedRequestHandler(
        IPropertyRepository propertyRepository,
        ILocationRepository locationRepository,
        IUserRepository userRepository,
        IPropertyImageRepository propertyImageRepository,
        ILogger<PropertyGetAllTopRatedRequestHandler> logger)
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
        this.userRepository = userRepository;
        this.propertyImageRepository = propertyImageRepository;
        this.logger = logger;
    }

    public async Task<IEnumerable<PropertyWithHeartAndRateDto>> Handle(PropertyGetAllTopRatedRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving top-rated properties");

        var properties = await propertyRepository
            .GetAll(m => m.DeletedBy == null)
            .OrderByDescending(x => x.Rate)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        if (properties == null || !properties.Any())
        {
            logger.LogWarning("No top-rated properties found");
            return new List<PropertyWithHeartAndRateDto>();
        }

        var ratedDtoList = new List<PropertyWithHeartAndRateDto>();

        foreach (var property in properties)
        {
            logger.LogInformation("Processing property ID: {PropertyId}", property.Id);

            logger.LogInformation("Retrieving location details for property ID: {PropertyId}", property.Id);
            var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);

            logger.LogInformation("Retrieving property image details for property ID: {PropertyId}", property.Id);
            var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);
            var firstPropertyImage = propertyImageDetails.FirstOrDefault();

            logger.LogInformation("Checking if property ID: {PropertyId} is liked by the user", property.Id);
            var isLiked = await userRepository.IsPropertyLikedByUserAsync(property.Id, cancellationToken);

            logger.LogInformation("Retrieving user details for host ID: {HostId}", property.CreatedBy);
            var user = await userRepository.GetAsync(x => x.Id == property.CreatedBy);

            var latestDto = new PropertyWithHeartAndRateDto
            {
                PropertyId = property.Id,
                Name = property.Name,
                City = locationDetails.City,
                Country = locationDetails.Country,
                Address = locationDetails.Address,
                IsLiked = isLiked,
                Rate = property.Rate,
                Image = firstPropertyImage?.Image,
                Url = firstPropertyImage?.Url ?? "/uploads/default/property_avatar.jpg",
                HostId = property.CreatedBy.Value,
                HostProfileImgUrl = user?.ProfileImgUrl
            };

            ratedDtoList.Add(latestDto);
        }

        logger.LogInformation("Successfully retrieved and processed top-rated properties");

        return ratedDtoList;
    }
}
