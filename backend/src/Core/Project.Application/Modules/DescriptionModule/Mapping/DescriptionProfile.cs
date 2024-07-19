using AutoMapper;
using Project.Application.Modules.AmenitiessModule.Queries;
using Project.Application.Modules.DescriptionModule.Queries;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.DescriptionModule.Mapping
{
    public class DescriptionProfile : Profile
    {
        public DescriptionProfile()
        {
            CreateMap<Description, DescriptionDto>();
        }
    }
}
