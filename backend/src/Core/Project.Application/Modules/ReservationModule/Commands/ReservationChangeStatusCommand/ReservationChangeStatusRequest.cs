using MediatR;


namespace Project.Application.Modules.ReservationModule.Commands.ReservationChangeStatusCommand
{
    public class ReservationChangeStatusRequest : IRequest
    {
        public int ReservationId { get; set; }
        public int ReservationStatus { get; set; }
    }
}
