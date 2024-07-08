using MediatR;

namespace Project.Application.Modules.AccountModule.Commands.EmailConfirmationCommand
{
    public class EmailConfirmationRequest : IRequest
    {
        public string Token { get; set; }
    }
}
