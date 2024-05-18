using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionRemoveCommand
{
    public class DescriptionRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
