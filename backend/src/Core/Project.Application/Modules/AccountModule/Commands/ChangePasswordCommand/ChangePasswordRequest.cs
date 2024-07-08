using MediatR;

namespace Project.Application.Modules.AccountModule.Commands.ChangePasswordCommand
{
    public class ChangePasswordRequest : IRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
