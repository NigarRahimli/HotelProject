using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetAllQuery
{
    class ReservationGetAllRequestHandler : IRequestHandler<ReservationGetAllRequest, IEnumerable<Reservation>>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly ILogger<ReservationGetAllRequestHandler> logger;

        public ReservationGetAllRequestHandler(IReservationRepository reservationRepository, ILogger<ReservationGetAllRequestHandler> logger)
        {
            this.reservationRepository = reservationRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<Reservation>> Handle(ReservationGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling request to get all reservations");

            var entities = await reservationRepository
                .GetAll(m => m.DeletedBy == null)
                .ToListAsync(cancellationToken);

            logger.LogInformation("{Count} reservations found", entities.Count);

            return entities;
        }
    }
}
