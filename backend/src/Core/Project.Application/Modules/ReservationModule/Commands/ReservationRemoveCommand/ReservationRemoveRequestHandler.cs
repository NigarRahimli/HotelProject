using MediatR;
using Project.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReservationsModule.Commands.ReservationRemoveCommand
{
    class ReservationRemoveRequestHandler : IRequestHandler<ReservationRemoveRequest>
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationRemoveRequestHandler(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        public async Task Handle(ReservationRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await reservationRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            reservationRepository.Remove(entity);
            await reservationRepository.SaveAsync(cancellationToken);
        }
    }
}
