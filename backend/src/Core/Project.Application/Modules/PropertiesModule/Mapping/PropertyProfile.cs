using AutoMapper;
using Project.Application.Modules.PropertiesModule.Commands;
using Project.Application.Modules.PropertiesModule.Queries;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllFeatured;
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

        

            CreateMap<Property, PropertyFilteredDto>()
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
                .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.LongPrice));

            CreateMap<Paginate<Property>, Paginate<PropertyFilteredDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
