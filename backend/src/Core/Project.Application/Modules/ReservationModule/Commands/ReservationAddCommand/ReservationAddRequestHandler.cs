using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;

namespace Project.Application.Modules.ReservationModule.Commands.ReservationAddCommand
{
    public class ReservationAddRequestHandler : IRequestHandler<ReservationAddRequest, Reservation>
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationAddRequestHandler(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task<Reservation> Handle(ReservationAddRequest request, CancellationToken cancellationToken)
        {
            var reservation = new Reservation
            {
                Name = request.Name,
                CheckInTime = request.CheckInTime,
                CheckOutTime = request.CheckOutTime,
                PropertyId = request.PropertyId,
                ReservationStatus = ReservationStatus.Upcoming,
                PaymentOption = (PaymentOption)request.PaymentOption
            };
            await reservationRepository.AddAsync(reservation, cancellationToken);
            await reservationRepository.SaveAsync(cancellationToken);
            return reservation;
        }
    }
}
