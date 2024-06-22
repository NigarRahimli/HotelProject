using AutoMapper;
using Project.Application.Modules.PropertiesModule.Commands;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;



namespace Project.Application.Modules.PropertiesModule.Mapping
{
    public class PropertyImageProfile : Profile
    {
        public PropertyImageProfile()
        {               
            CreateMap<Property, PropertyDto>();
                //.ForMember(dest => dest.PropertyName, src => src.MapFrom(m => m.Name))
                //.ForMember(dest => dest.CreatedTime, src => src.MapFrom(m => m.CreatedAt));

            CreateMap<Paginate<Property>, Paginate<PropertyDto>>();
        }
    }
}
