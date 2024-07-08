using MediatR;


namespace Project.Application.Modules.AccountModule.Commands.SendForgotPasswordEmailCommand
{
    public class SendForgotPasswordEmailRequest:IRequest
    {
        public string Email { get; set; }
    }
}
