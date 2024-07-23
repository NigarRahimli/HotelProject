using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;


namespace Project.Application.Modules.ReservationsModule.Commands.ReservationRemoveCommand
{
    class ReservationRemoveRequestHandler : IRequestHandler<ReservationRemoveRequest>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly ILogger<ReservationRemoveRequestHandler> logger;

        public ReservationRemoveRequestHandler(IReservationRepository reservationRepository, ILogger<ReservationRemoveRequestHandler> logger)
        {
            this.reservationRepository = reservationRepository;
            this.logger = logger;
        }

        public async Task Handle(ReservationRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling reservation removal for ReservationId: {ReservationId}", request.Id);

            var entity = await reservationRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy==null, cancellationToken);

           
            reservationRepository.Remove(entity);
            await reservationRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Successfully removed reservation with ReservationId: {ReservationId}", request.Id);
        }
    }
}
