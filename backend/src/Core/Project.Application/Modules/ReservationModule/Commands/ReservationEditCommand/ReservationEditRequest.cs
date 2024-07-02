using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReservationsModule.Commands.ReservationEditCommand
{
    public class ReservationEditRequest : IRequest<Reservation>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int PaymentOption { get; set; }
    }
}
