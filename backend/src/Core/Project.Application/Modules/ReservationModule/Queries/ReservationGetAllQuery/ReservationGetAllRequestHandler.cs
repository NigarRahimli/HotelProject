using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetAllQuery
{
    class ReservationGetAllRequestHandler : IRequestHandler<ReservationGetAllRequest, IEnumerable<Reservation>>
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationGetAllRequestHandler(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        public async Task<IEnumerable<Reservation>> Handle(ReservationGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await reservationRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
