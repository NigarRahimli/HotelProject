using MediatR;
using System.Security.Claims;

namespace Resume.Application.Modules.AccountModule.Commands.SigninCommand
{
    public class SigninRequest : IRequest<ClaimsPrincipal>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
