using MediatR;
using System.Security.Claims;

namespace Resume.Application.Modules.AccountModule.Commands.SigninCommand
{
    public class SigninRequest : IRequest<ClaimsPrincipal>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
