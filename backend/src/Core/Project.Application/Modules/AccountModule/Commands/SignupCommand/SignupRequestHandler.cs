using Project.Infrastructure.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Web;
using Microsoft.Extensions.Logging;

namespace Project.Application.Modules.AccountModule.Commands.SignupCommand
{
    public class SignupRequestHandler : IRequestHandler<SignupRequest>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IActionContextAccessor ctx;
        private readonly IEmailService emailService;
        private readonly ICryptoService cryptoService;
        private readonly ILogger<SignupRequestHandler> logger;

        public SignupRequestHandler(UserManager<AppUser> userManager, IActionContextAccessor ctx, IEmailService emailService, ICryptoService cryptoService, ILogger<SignupRequestHandler> logger)
        {
            this.userManager = userManager;
            this.ctx = ctx;
            this.emailService = emailService;
            this.cryptoService = cryptoService;
            this.logger = logger;
        }

        public async Task Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("SignupRequestHandler started handling request for email: {Email}", request.Email);

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is not null)
            {
                
                logger.LogWarning("Email {Email} is already taken", request.Email);
                throw new EntityAlreadyExistsException(nameof(AppUser),request.Email);
            }

            user = new AppUser
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                EmailConfirmed = false,
                UserName = $"{request.Name}.{request.Surname}".ToLower(),
                ProfileImgUrl = "/uploads/default/profile_avatar.png"
            };

            var sameUserName = await userManager.FindByNameAsync(user.UserName);
            if (sameUserName is not null)
            {
                var maxCount = userManager.Users.Count(m => m.UserName.StartsWith(user.UserName));
                user.UserName = $"{request.Name}.{request.Surname}{maxCount + 1}".ToLower();
                logger.LogInformation("Generated new username for {Email}: {UserName}", request.Email, user.UserName);
            }

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(k => k.Code, v => (IEnumerable<string>)new[] { v.Description });
                logger.LogError("User creation failed for {Email} with errors: {Errors}", request.Email, errors);
                throw new BadRequestException("One or more errors occurred!", errors);
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            token = cryptoService.Encrypt($"{user.Email}-{token}");
            token = HttpUtility.UrlEncode(token);

            var confirmationUrl = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/api/account/confirm-email?token={token}";

            await emailService.SendEmailAsync(request.Email, "Project Registration", @$"Hello, Dear Customer.<br/>Please <a href='{confirmationUrl}'>Confirm</a> your email");

            logger.LogInformation("SignupRequestHandler successfully completed request for email: {Email}", request.Email);
        }
    }
}
