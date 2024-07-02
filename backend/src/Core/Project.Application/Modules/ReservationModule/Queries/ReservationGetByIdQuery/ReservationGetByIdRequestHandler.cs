using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetByIdQuery
{
    class ReservationGetByIdRequestHandler : IRequestHandler<ReservationGetByIdRequest, Reservation>
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationGetByIdRequestHandler(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        public async Task<Reservation> Handle(ReservationGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await reservationRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            return entity;
        }
    }
}
