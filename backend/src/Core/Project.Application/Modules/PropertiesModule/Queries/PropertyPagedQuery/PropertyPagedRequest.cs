using MediatR;
using Project.Application.Modules.PropertiesModule.Commands;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Concretes;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyPagedQuery
{
    public class PropertyPagedRequest : Pageable, IPageable, IRequest<IPaginate<PropertyDto>>
    {
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int? GuestNum { get; set; }
        public int? KindId { get; set; }
        public string? CityName { get; set; }

    }
}
