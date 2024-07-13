using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            AppUser user = null;

            // Check if the login is an email
            if (request.Login.Contains("@"))
            {
                user = await userManager.FindByEmailAsync(request.Login);
                if (user != null && !user.EmailConfirmed)
                    throw new NotConfirmedException("email");
            }
            else
            {
                // Check if the login is a phone number
                user = await userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == request.Login, cancellationToken);
                if (user != null && !user.PhoneNumberConfirmed)
                    throw new NotConfirmedException("phone");

                if (user == null)
                {
                    // Assume the login is a username
                    user = await userManager.FindByNameAsync(request.Login);
                    if (user != null && !user.EmailConfirmed)
                        throw new NotConfirmedException("email");
                }
            }

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
