using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Security.Claims;

namespace Resume.Application.Modules.AccountModule.Commands.SigninCommand
{
    class SigninRequestHandler : IRequestHandler<SigninRequest, ClaimsPrincipal>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signinManager;

        public SigninRequestHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
        }


        public async Task<ClaimsPrincipal> Handle(SigninRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserName);

            if (user is null)
                throw new UserNotFoundException();

            var hasher = new PasswordHasher<AppUser>();


            if (hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                return new ClaimsPrincipal(claimsIdentity);
            }
            else
                throw new UserNotFoundException();
        }
    }
}
