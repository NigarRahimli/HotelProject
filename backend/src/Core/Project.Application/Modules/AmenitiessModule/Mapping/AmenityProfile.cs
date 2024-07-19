using AutoMapper;
using Project.Application.Modules.AmenitiessModule.Queries;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.AmenitiessModule.Mapping
{
    public class AmenityProfile : Profile
    {
        public AmenityProfile()
        {
            CreateMap<Amenity, AmenityDto>();
        }
    }
}
