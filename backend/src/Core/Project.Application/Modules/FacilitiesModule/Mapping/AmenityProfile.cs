using AutoMapper;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetAllQuery;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilitiesModule.Mapping
{
    public class FacilityProfile : Profile
    {
        public FacilityProfile()
        {
            CreateMap<Facility, FacilityAllDto>();
        }
    }
}
