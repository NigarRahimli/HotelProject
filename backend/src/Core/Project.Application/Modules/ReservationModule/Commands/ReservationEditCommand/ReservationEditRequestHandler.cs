using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;


namespace Project.Application.Modules.ReservationsModule.Commands.ReservationEditCommand
{
    class ReservationEditRequestHandler : IRequestHandler<ReservationEditRequest, Reservation>
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationEditRequestHandler(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        public async Task<Reservation> Handle(ReservationEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await reservationRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            entity.PaymentOption = (PaymentOption)request.PaymentOption;
            entity.CheckOutTime=request.CheckOutTime;
            entity.CheckInTime=request.CheckInTime;
           
            await reservationRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
