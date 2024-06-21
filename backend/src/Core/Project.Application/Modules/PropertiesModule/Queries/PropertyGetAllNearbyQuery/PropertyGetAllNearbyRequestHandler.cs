using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery;
using Project.Application.Repositories;
using Project.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class PropertyGetAllNearbyRequestHandler : IRequestHandler<PropertyGetAllNearbyRequest, IEnumerable<PropertyWithHeartDto>>
{
    private readonly IPropertyRepository propertyRepository;
    private readonly ILocationRepository locationRepository;

    public PropertyGetAllNearbyRequestHandler(IPropertyRepository propertyRepository, ILocationRepository locationRepository)
    {
        this.propertyRepository = propertyRepository;
        this.locationRepository = locationRepository;
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

                var nearbyDto = new PropertyWithHeartDto
                {
                    PropertyId = property.Id,
                    Name = property.Name,
                    City = locationDetails.City,
                    Country = locationDetails.Country,
                    Address = locationDetails.Address
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
