using MediatR;
using System.Security.Claims;

namespace Project.Application.Modules.AccountModule.Commands.SigninCommand
{
    public class SigninRequest : IRequest<ClaimsPrincipal>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
