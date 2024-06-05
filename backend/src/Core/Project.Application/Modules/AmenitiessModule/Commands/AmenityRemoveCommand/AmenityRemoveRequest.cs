using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityRemoveCommand
{
    public class AmenityRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
