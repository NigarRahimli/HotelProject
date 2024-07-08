using AutoMapper;
using Project.Application.Modules.KindsModule.Queries;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.KindsModule.Mapping
{
    public class KindProfile : Profile
    {
        public KindProfile()
        {
            CreateMap<Kind, KindDto>();
        }
    }
}
