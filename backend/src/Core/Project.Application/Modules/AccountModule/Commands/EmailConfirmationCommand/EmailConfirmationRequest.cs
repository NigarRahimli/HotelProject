using MediatR;

namespace Project.Application.Modules.AccountModule.Commands.EmailConfirmationCommand
{
    public class EmailConfirmationRequest : IRequest<Unit>
    {
        public string Token { get; set; }
    }
}
