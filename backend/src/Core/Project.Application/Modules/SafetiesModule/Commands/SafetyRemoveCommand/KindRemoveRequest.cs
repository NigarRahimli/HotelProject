using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyRemoveCommand
{
    public class SafetyRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
