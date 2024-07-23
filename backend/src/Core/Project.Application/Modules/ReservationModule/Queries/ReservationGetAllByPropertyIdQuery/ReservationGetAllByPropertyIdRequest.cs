using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetAllByPropertyIdQuery
{
    public class ReservationGetAllByPropertyIdRequest : IRequest<IEnumerable<ReservationDto>>
    {
        public int PropertyId { get; set; }
    }
}
