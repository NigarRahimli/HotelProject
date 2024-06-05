using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyRemoveCommand
{
    public class PropertyRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
