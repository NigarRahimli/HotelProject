using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Exceptions;
using System.Web;

namespace Project.Application.Modules.AccountModule.Commands.ResendConfirmationEmailCommand
{
    public class ResendConfirmationEmailRequestHandler : IRequestHandler<ResendConfirmationEmailRequest>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IActionContextAccessor ctx;
        private readonly IEmailService emailService;
        private readonly ICryptoService cryptoService;

        public ResendConfirmationEmailRequestHandler(UserManager<AppUser> userManager, IActionContextAccessor ctx, IEmailService emailService, ICryptoService cryptoService)
        {
            this.userManager = userManager;
            this.ctx = ctx;
            this.emailService = emailService;
            this.cryptoService = cryptoService;
        }

        public async Task Handle(ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException("Email not found");
            }

            if (user.EmailConfirmed)
            {
                throw new BadRequestException("Email is already confirmed");
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            token = cryptoService.Encrypt($"{user.Email}-{token}");
            token = HttpUtility.UrlEncode(token);

            var confirmationUrl = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/api/account/confirm-email?token={token}";

            await emailService.SendEmailAsync(request.Email, "Project Registration", @$"Hello, Dear Customer.<br/>Please <a href='{confirmationUrl}'>Confirm</a> your email");

        }
    }
}
