using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReservationsModule.Commands.ReservationRemoveCommand
{
    public class ReservationRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
