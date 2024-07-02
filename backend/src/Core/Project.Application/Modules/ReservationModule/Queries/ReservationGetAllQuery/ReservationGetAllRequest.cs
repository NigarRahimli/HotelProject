using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetAllQuery
{
    public class ReservationGetAllRequest : IRequest<IEnumerable<Reservation>>
    {
    }
}
