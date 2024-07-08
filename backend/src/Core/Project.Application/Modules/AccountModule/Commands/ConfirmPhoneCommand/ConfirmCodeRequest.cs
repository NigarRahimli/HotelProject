using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.ConfirmPhoneCommand
{
    public class ConfirmCodeRequest:IRequest
    {
        public string ConfirmationCode { get; set; }
    }
}
