using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.KindsModule.Commands.KindRemoveCommand
{
    public class KindRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
