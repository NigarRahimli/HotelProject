using AutoMapper;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetAllByPropertyIdQuery;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReservationModule.Mapping
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.ReservationStatusName, opt => opt.MapFrom(src => src.ReservationStatus.ToString()));
        }
    }
}
