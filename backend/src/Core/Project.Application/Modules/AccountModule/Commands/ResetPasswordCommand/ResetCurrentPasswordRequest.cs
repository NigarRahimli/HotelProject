

using MediatR;

namespace Project.Application.Modules.AccountModule.Commands.ResetPasswordCommand
{
    public class ResetCurrentPasswordRequest:IRequest
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string Token { get; set; }
    }
}
