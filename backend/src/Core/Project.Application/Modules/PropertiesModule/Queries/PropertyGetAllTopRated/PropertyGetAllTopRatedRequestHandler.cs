using MediatR;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Repositories;
using Project.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllTopRated;

public class PropertyGetAllTopRatedRequestHandler : IRequestHandler<PropertyGetAllTopRatedRequest, IEnumerable<PropertyWithHeartAndRateDto>>
{
    private readonly IPropertyRepository propertyRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IUserRepository userRepository;
    private readonly IPropertyImageRepository propertyImageRepository;

    public PropertyGetAllTopRatedRequestHandler(
        IPropertyRepository propertyRepository,
        ILocationRepository locationRepository,
        IUserRepository userRepository,
        IPropertyImageRepository propertyImageRepository)
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
        this.userRepository = userRepository;
        this.propertyImageRepository = propertyImageRepository;
    }

    public async Task<IEnumerable<PropertyWithHeartAndRateDto>> Handle(PropertyGetAllTopRatedRequest request, CancellationToken cancellationToken)
    {
        var properties = await propertyRepository
            .GetAll(m => m.DeletedBy == null)
            .OrderByDescending(x => x.Rate)
            .Take(request.Take)
            .ToListAsync(cancellationToken);



        var ratedDtoList = new List<PropertyWithHeartAndRateDto>();
        foreach (var property in properties)
        {
            try
            {
                var locationDetails = await locationRepository.GetLocationDetailsAsync(property.LocationId, cancellationToken);

                var propertyImageDetails = await propertyImageRepository.GetPropertyImageDetailsAsync(property.Id, cancellationToken);
                var firstPropertyImage = propertyImageDetails.FirstOrDefault();
                var isLiked = await userRepository.IsPropertyLikedByUserAsync(property.Id, cancellationToken);

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
                    Url = firstPropertyImage?.Url
                };

                ratedDtoList.Add(latestDto);


            }
            catch (NotFoundException ex)
            {
                throw new Exception($"Failed to get location details for property with ID {property.Id}: {ex.Message}");
            }
            catch (Exception ex)
            {

                throw new Exception($"Error retrieving property image details for property with ID {property.Id}: {ex.Message}");
            }
        }

        return ratedDtoList;
    }
}
