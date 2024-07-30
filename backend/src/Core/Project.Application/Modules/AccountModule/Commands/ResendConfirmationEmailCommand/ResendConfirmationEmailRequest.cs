using MediatR;

namespace Project.Application.Modules.AccountModule.Commands.ResendConfirmationEmailCommand
{
    public class ResendConfirmationEmailRequest : IRequest
    {
        public string Email { get; set; }
    }
}
