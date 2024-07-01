using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.LocationsModule.Commands.LocationRemoveCommand
{
    public class LocationRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
