using MediatR;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;

namespace Project.Application.Modules.ReservationModule.Commands.ReservationAddCommand
{
    public class ReservationAddRequest : IRequest<Reservation>
    {
        public string Name { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int UserId { get; set; }
        public int HostId { get; set; }
        public int PropertyId { get; set; }
        public int PaymentOption { get; set; }
    }
}
