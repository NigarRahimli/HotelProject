using MediatR;
using System.Security.Claims;

namespace Resume.Application.Modules.AccountModule.Commands.PrincipalFillCommand
{
    public class PrincipalFillRequest : IRequest
    {
        public PrincipalFillRequest(ClaimsIdentity identity) => Identity = identity;

        internal ClaimsIdentity Identity { get; set; }
    }
}
