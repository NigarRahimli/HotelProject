using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.Application.Modules.AccountModule.Commands.SendForgotPasswordEmailCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using System.Web;

public class SendForgotPasswordEmailRequestHandler : IRequestHandler<SendForgotPasswordEmailRequest>
{
    private readonly IActionContextAccessor ctx;
    private readonly UserManager<AppUser> userManager;
    private readonly IEmailService emailService;
    private readonly ICryptoService cryptoService;

    public SendForgotPasswordEmailRequestHandler(
        IActionContextAccessor ctx, UserManager<AppUser> userManager, IEmailService emailService, ICryptoService cryptoService)
    {
        this.ctx = ctx;
        this.userManager = userManager;
        this.emailService = emailService;
        this.cryptoService = cryptoService;
    }

    public async Task Handle(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new Exception("User not found");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var encryptedToken = cryptoService.Encrypt(token);
        var encodedToken = HttpUtility.UrlEncode(encryptedToken);
        var resetLink = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/api/account/reset-password/{request.Email}/{encodedToken}";

        await emailService.SendEmailAsync(request.Email, "Reset Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");
   
    }
}
