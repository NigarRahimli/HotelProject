using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Application.Utils;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;
using Twilio.TwiML.Voice;

namespace Project.Application.Modules.ReservationModule.Commands.ReservationAddCommand
{
    public class ReservationAddRequestHandler : IRequestHandler<ReservationAddRequest, Reservation>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<ReservationAddRequestHandler> logger;

        public ReservationAddRequestHandler(IReservationRepository reservationRepository, IPropertyRepository propertyRepository, ILogger<ReservationAddRequestHandler> logger)
        {
            this.reservationRepository = reservationRepository;
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task<Reservation> Handle(ReservationAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReservationAddRequest for PropertyId {PropertyId}", request.PropertyId);

            var property = await propertyRepository.GetAsync(x => x.Id == request.PropertyId && x.DeletedBy == null, cancellationToken);

            logger.LogInformation("Checked property with PropertyId {PropertyId} exists", request.PropertyId);

            if (await reservationRepository.IsReservationTimeFrameAvailable(request.PropertyId, request.CheckInTime, request.CheckOutTime, cancellationToken))
            {
                var reservation = new Reservation
                {
                    Name = request.Name,
                    CheckInTime = request.CheckInTime,
                    CheckOutTime = request.CheckOutTime,
                    PropertyId = request.PropertyId,
                    PaymentOption = (PaymentOption)request.PaymentOption,
                    TotalAmount =  ReservationUtils.CalculateTotalAmount(request.CheckInTime, request.CheckOutTime, property)
                };
                await reservationRepository.AddAsync(reservation, cancellationToken);
                await reservationRepository.SaveAsync(cancellationToken);
                logger.LogInformation("Reservation created with Id {ReservationId} for PropertyId {PropertyId}", reservation.Id, request.PropertyId);

                return reservation;
            }
            else
            {
                logger.LogWarning("The reservation time frame overlaps with an existing approved reservation for PropertyId {PropertyId}", request.PropertyId);
                throw new InvalidOperationException("The reservation time frame overlaps with an existing approved reservation.");
            }
        }



        
    }
}
