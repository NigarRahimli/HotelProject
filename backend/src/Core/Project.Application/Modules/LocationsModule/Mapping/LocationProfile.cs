using AutoMapper;
using Project.Application.Modules.LocationsModule.Queries.LocationGetByUserIdQuery;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.LocationsModule.Mapping
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationByUserDto>();
        }
    }
}
