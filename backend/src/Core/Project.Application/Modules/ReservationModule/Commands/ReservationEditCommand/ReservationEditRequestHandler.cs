using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Application.Utils;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;

namespace Project.Application.Modules.ReservationsModule.Commands.ReservationEditCommand
{
    public class ReservationEditRequestHandler : IRequestHandler<ReservationEditRequest, Reservation>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<ReservationEditRequestHandler> logger;

        public ReservationEditRequestHandler(IReservationRepository reservationRepository, ILogger<ReservationEditRequestHandler> logger, IPropertyRepository propertyRepository)
        {
            this.reservationRepository = reservationRepository;
            this.logger = logger;
            this.propertyRepository = propertyRepository;
        }

        public async Task<Reservation> Handle(ReservationEditRequest request, CancellationToken cancellationToken)
        {
     

            var entity = await reservationRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            var property = await propertyRepository.GetAsync(x => x.Id == entity.PropertyId && x.DeletedBy == null, cancellationToken);




            if (!await reservationRepository.IsReservationTimeFrameAvailable(entity.PropertyId, request.CheckInTime, request.CheckOutTime, cancellationToken))
            {
                logger.LogWarning("The edited reservation time frame overlaps with an existing approved reservation for PropertyId {PropertyId}", entity.PropertyId);
                throw new InvalidOperationException("The edited reservation time frame overlaps with an existing approved reservation.");
            }

           
            entity.Name = request.Name;
            entity.CheckInTime = request.CheckInTime;
            entity.CheckOutTime = request.CheckOutTime;
            entity.PaymentOption = (PaymentOption)request.PaymentOption;
            entity.TotalAmount = ReservationUtils.CalculateTotalAmount( request.CheckInTime, request.CheckOutTime,property);



            await reservationRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Reservation with Id {ReservationId} updated successfully", entity.Id);

            return entity;
        }
    }
}
