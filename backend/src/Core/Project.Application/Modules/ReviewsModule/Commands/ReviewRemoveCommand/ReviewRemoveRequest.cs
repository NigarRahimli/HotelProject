using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewModule.Commands.ReviewRemoveCommand
{
    public class ReviewRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
