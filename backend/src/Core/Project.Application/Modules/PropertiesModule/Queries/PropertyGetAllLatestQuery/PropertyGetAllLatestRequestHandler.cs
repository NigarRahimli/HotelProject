using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllLatestQuery;
using Project.Application.Repositories;
using Project.Infrastructure.Exceptions;

public class PropertyGetAllLatestRequestHandler : IRequestHandler<PropertyGetAllLatestRequest, IEnumerable<PropertyWithHeartDto>>
{
    private readonly IPropertyRepository propertyRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IUserRepository userRepository;
    private readonly IPropertyImageRepository propertyImageRepository;
    private readonly ILogger<PropertyGetAllLatestRequestHandler> logger;

    public PropertyGetAllLatestRequestHandler(
        IPropertyRepository propertyRepository,
        ILocationRepository locationRepository,
        IUserRepository userRepository,
        IPropertyImageRepository propertyImageRepository,
        ILogger<PropertyGetAllLatestRequestHandler> logger
    )
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
        this.userRepository = userRepository;
        this.propertyImageRepository = propertyImageRepository;
        this.logger = logger;
    }

    public async Task<IEnumerable<PropertyWithHeartDto>> Handle(PropertyGetAllLatestRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving latest properties, take: {Take}", request.Take);
        var properties = await propertyRepository
            .GetAll(m => m.DeletedBy == null)
            .OrderByDescending(x => x.Id)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        var latestDtoList = new List<PropertyWithHeartDto>();
        foreach (var property in properties)
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

                var latestDto = new PropertyWithHeartDto
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

                latestDtoList.Add(latestDto);

            logger.LogInformation("Property {Id} added successfully to latest properties ", property.Id);
           
        }
        logger.LogInformation("PropertyGetAllLatestRequest handled successfully ");


        return latestDtoList;
    }
}
