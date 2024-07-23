using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetAllByPropertyIdQuery;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetAllQuery
{
    class ReservationGetAllByPropertyIdRequestHandler : IRequestHandler<ReservationGetAllByPropertyIdRequest, IEnumerable<ReservationDto>>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ReservationGetAllByPropertyIdRequestHandler> logger;

        public ReservationGetAllByPropertyIdRequestHandler(
            IReservationRepository reservationRepository,
            IMapper mapper,
            IPropertyRepository propertyRepository,
            ILogger<ReservationGetAllByPropertyIdRequestHandler> logger)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(ReservationGetAllByPropertyIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling request to get all reservations by property ID: {PropertyId}", request.PropertyId);

            var property = await propertyRepository.GetAsync(x => x.Id == request.PropertyId, cancellationToken);

            logger.LogInformation("Property with ID {PropertyId} found", request.PropertyId);

            var entities = await reservationRepository
                .GetAll(m => m.DeletedBy == null && m.PropertyId == request.PropertyId)
                .ToListAsync(cancellationToken);

            logger.LogInformation("{Count} reservations found for property ID {PropertyId}", entities.Count, request.PropertyId);

            var reservationDtos = mapper.Map<IEnumerable<ReservationDto>>(entities);
            logger.LogInformation("Mapped reservations to DTOs");

            return reservationDtos;
        }
    }
}
