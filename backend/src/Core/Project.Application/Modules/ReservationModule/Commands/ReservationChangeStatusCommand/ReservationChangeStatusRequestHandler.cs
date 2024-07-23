using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Enums;
using Project.Infrastructure.Exceptions;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.ReservationModule.Commands.ReservationChangeStatusCommand
{
    public class ReservationChangeStatusRequestHandler : IRequestHandler<ReservationChangeStatusRequest>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<ReservationChangeStatusRequestHandler> logger;

        public ReservationChangeStatusRequestHandler(
            IReservationRepository reservationRepository,
            IHttpContextAccessor contextAccessor,
            IPropertyRepository propertyRepository,
            ILogger<ReservationChangeStatusRequestHandler> logger)
        {
            this.reservationRepository = reservationRepository;
            this.contextAccessor = contextAccessor;
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task Handle(ReservationChangeStatusRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReservationChangeStatusRequest for ReservationId: {ReservationId}", request.ReservationId);

            var reservation = await reservationRepository.GetAsync(x => x.Id == request.ReservationId, cancellationToken);
            if (reservation == null)
            {
                logger.LogWarning("Reservation with Id: {ReservationId} not found.", request.ReservationId);
                throw new NotFoundException("Reservation not found.");
            }

            if (reservation.ReservationStatus == (ReservationStatus)request.ReservationStatus)
            {
                logger.LogWarning("Reservation with Id: {ReservationId} already has the status: {Status}.", request.ReservationId, request.ReservationStatus);
                throw new InvalidOperationException("The reservation already has this status.");
            }

            var property = await propertyRepository.GetAsync(x => x.Id == reservation.PropertyId, cancellationToken);
            if (property == null)
            {
                logger.LogWarning("Property with Id: {PropertyId} not found.", reservation.PropertyId);
                throw new NotFoundException("Property not found.");
            }

            var userId = contextAccessor.HttpContext.GetUserIdExtension();
            if (userId == property.CreatedBy)
            {
                // Only check for overlap if changing status to 1 (approved)
                if ((ReservationStatus)request.ReservationStatus == ReservationStatus.Approved)
                {
                    var isAvailable = await reservationRepository.IsReservationTimeFrameAvailable(
                        reservation.PropertyId,
                        reservation.CheckInTime,
                        reservation.CheckOutTime,
                        cancellationToken
                    );

                    if (!isAvailable)
                    {
                        logger.LogWarning("The reservation overlaps with another approved reservation for PropertyId: {PropertyId}.", reservation.PropertyId);
                        throw new InvalidOperationException("The reservation overlaps with another approved reservation.");
                    }
                }

                logger.LogInformation("Changing status of ReservationId: {ReservationId} to {Status}.", request.ReservationId, request.ReservationStatus);
                reservation.ReservationStatus = (ReservationStatus)request.ReservationStatus;
            }
            else
            {
                logger.LogWarning("User with Id: {UserId} is not the owner of PropertyId: {PropertyId}.", userId, reservation.PropertyId);
                throw new OwnerAccessException("Only the owner of the property can change the reservation status.");
            }

            await reservationRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Status of ReservationId: {ReservationId} changed successfully to {Status}.", request.ReservationId, request.ReservationStatus);
        }
    }
}
