using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Entities.Membership;

namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetByIdQuery
{
    class ReservationGetByIdRequestHandler : IRequestHandler<ReservationGetByIdRequest, ReservationDetailedDto>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly ILogger<ReservationGetByIdRequestHandler> logger;

        public ReservationGetByIdRequestHandler(IReservationRepository reservationRepository, IPropertyRepository propertyRepository, UserManager<AppUser> userManager, ILogger<ReservationGetByIdRequestHandler> logger)
        {
            this.reservationRepository = reservationRepository;
            this.propertyRepository = propertyRepository;
            this.userManager = userManager;
            this.logger = logger;
        }
        public async Task<ReservationDetailedDto> Handle(ReservationGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling request for Reservation ID: {ReservationId}", request.Id);
            var reservation = await reservationRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            var property= await propertyRepository.GetAsync(x=>x.Id==reservation.PropertyId && x.DeletedBy == null, cancellationToken);
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == reservation.CreatedBy,cancellationToken);
            var reservationDetailedDto = new ReservationDetailedDto()
            {
                UserName = user.Name,
                UserSurname = user.Surname,
                ProfileImgUrl = user.ProfileImgUrl,

                KindId = property.KindId,
                LocationId = property.LocationId,
                PropertyName = property.Name,

                ReservationId = reservation.Id,
                ReservationName = reservation.Name,
                CheckInTime = reservation.CheckInTime,
                CheckOutTime = reservation.CheckOutTime,
                ReservationStatusName = reservation.ReservationStatus.ToString(), 
                PropertyId = reservation.PropertyId,
                PaymentOptionName = reservation.PaymentOption.ToString(), 
                TotalAmount = reservation.TotalAmount
            };

            logger.LogInformation("Successfully retrieved reservation details for Reservation ID: {ReservationId}", request.Id);
            return reservationDetailedDto;
        }
    }
}
