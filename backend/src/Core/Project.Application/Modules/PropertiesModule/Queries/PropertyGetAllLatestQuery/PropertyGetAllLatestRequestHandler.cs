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
    private readonly IPropertyImageRepository propertyImageRepository;

    public PropertyGetAllLatestRequestHandler(
        IPropertyRepository propertyRepository,
        ILocationRepository locationRepository,
        IFUserRepository userRepository,
        IPropertyImageRepository propertyImageRepository)
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
        this.userRepository = userRepository;
        this.propertyImageRepository = propertyImageRepository;
    }

    public async Task<IEnumerable<PropertyWithHeartDto>> Handle(PropertyGetAllLatestRequest request, CancellationToken cancellationToken)
    {
        var properties = await propertyRepository
            .GetAll(m => m.DeletedBy == null)
            .OrderByDescending(x => x.Id)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        

        var latestDtoList = new List<PropertyWithHeartDto>();
        foreach (var property in properties)
        {
            try
            {
                var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);

                var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);
                var firstPropertyImage = propertyImageDetails.FirstOrDefault();
                var isLiked = await userRepository.IsPropertyLikedByUserAsync(request.UserId, property.Id, cancellationToken);

                var latestDto = new PropertyWithHeartDto
                    {
                        PropertyId = property.Id,
                        Name = property.Name,
                        City = locationDetails.City,
                        Country = locationDetails.Country,
                        Address = locationDetails.Address,
                        IsLiked = isLiked,
                        Image = firstPropertyImage?.Image,
                        Url = firstPropertyImage?.Url
                    };

                    latestDtoList.Add(latestDto);
           
           
            }
            catch (NotFoundException ex)
            {
                throw new Exception($"Failed to get location details for property with ID {property.Id}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions related to property image details retrieval
                throw new Exception($"Error retrieving property image details for property with ID {property.Id}: {ex.Message}");
            }
        }

        return latestDtoList;
    }
}
