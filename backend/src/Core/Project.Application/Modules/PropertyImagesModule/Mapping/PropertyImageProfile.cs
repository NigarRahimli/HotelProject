using AutoMapper;
using Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.PropertieImagesModule.Mapping
{
    public class PropertyImageProfile : Profile
    {
        public PropertyImageProfile()
        {
            CreateMap<PropertyImage, PropertyImageDetailsDto>();
        }
    }
}
