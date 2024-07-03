﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetAllByPropertyIdQuery;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetAllQuery
{
    class ReservationGetAllByPropertyIdRequestHandler : IRequestHandler<ReservationGetAllByPropertyIdRequest, IEnumerable<Reservation>>
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationGetAllByPropertyIdRequestHandler(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        public async Task<IEnumerable<Reservation>> Handle(ReservationGetAllByPropertyIdRequest request, CancellationToken cancellationToken)
        {
            var entities = await reservationRepository.GetAll(m => m.DeletedBy == null && m.PropertyId==request.PropertyId).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
