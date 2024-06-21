using MediatR;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllLatestQuery;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Repositories;
using Project.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

public class PropertyGetAllLatestRequestHandler : IRequestHandler<PropertyGetAllLatestRequest, IEnumerable<PropertyWithHeartDto>>
{
    private readonly IPropertyRepository propertyRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IFUserRepository userRepository;

    public PropertyGetAllLatestRequestHandler(
        IPropertyRepository propertyRepository,
        ILocationRepository locationRepository,
        IFUserRepository userRepository)
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
        this.userRepository = userRepository;
    }

    public async Task<IEnumerable<PropertyWithHeartDto>> Handle(PropertyGetAllLatestRequest request, CancellationToken cancellationToken)
    {
        var properties = await propertyRepository
            .GetAll(m => m.DeletedBy == null)
            .OrderByDescending(x => x.Id)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        var likedPropertyIds = await userRepository
            .GetLikedPropertyIdsAsync(request.UserId, cancellationToken);

        var latestDtoList = new List<PropertyWithHeartDto>();
        foreach (var property in properties)
        {
            try
            {
                var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);

                var latestDto = new PropertyWithHeartDto
                {
                    PropertyId = property.Id,
                    Name = property.Name,
                    City = locationDetails.City,
                    Country = locationDetails.Country,
                    Address = locationDetails.Address,
                    IsLiked = likedPropertyIds.Contains(property.Id)
                };

                latestDtoList.Add(latestDto);
            }
            catch (NotFoundException ex)
            {
                throw new Exception($"Failed to get location details for property with ID {property.Id}: {ex.Message}");
            }
        }

        return latestDtoList;
    }
}