using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace Project.Application.Modules.AccountModule.Commands.SigninCommand
{
    class SigninRequestHandler : IRequestHandler<SigninRequest, ClaimsPrincipal>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signinManager;
        private readonly ILogger<SigninRequestHandler> logger;

        public SigninRequestHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, ILogger<SigninRequestHandler> logger)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
            this.logger = logger;
        }

        public async Task<ClaimsPrincipal> Handle(SigninRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("SigninRequestHandler started handling request for login: {Login}", request.Login);

            AppUser user = null;

            if (request.Login.Contains("@"))
            {
                user = await userManager.FindByEmailAsync(request.Login);
                if (user != null && !user.EmailConfirmed)
                {
                    logger.LogWarning("Email not confirmed for user with email: {Email}", request.Login);
                    throw new NotConfirmedException("email");
                }
            }
            else
            {
                user = await userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == request.Login, cancellationToken);
                if (user != null && !user.PhoneNumberConfirmed)
                {
                    logger.LogWarning("Phone number not confirmed for user with phone number: {PhoneNumber}", request.Login);
                    throw new NotConfirmedException("phone");
                }

                if (user == null)
                {
                    user = await userManager.FindByNameAsync(request.Login);
                    if (user != null && !user.EmailConfirmed)
                    {
                        logger.LogWarning("Email not confirmed for user with username: {Username}", request.Login);
                        throw new NotConfirmedException("email");
                    }
                }
            }

            if (user is null)
            {
                logger.LogError("User not found for login: {Login}", request.Login);
                throw new UserNotFoundException();
            }

            var hasher = new PasswordHasher<AppUser>();

            if (hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                logger.LogInformation("SigninRequestHandler successfully handled request for login: {Login}", request.Login);

                return new ClaimsPrincipal(claimsIdentity);
            }
            else
            {
                logger.LogError("Invalid password for login: {Login}", request.Login);
                throw new UserNotFoundException();
            }
        }
    }
}
