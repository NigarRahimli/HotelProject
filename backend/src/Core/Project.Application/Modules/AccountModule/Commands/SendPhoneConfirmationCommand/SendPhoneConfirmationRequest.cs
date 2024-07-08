using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.SendPhoneConfirmationCommand
{
    public class SendPhoneConfirmationRequest:IRequest
    {
        public string PhoneNumber { get; set; }
    }
}
