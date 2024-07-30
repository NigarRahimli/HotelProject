using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Project.Application.Modules.AccountModule.Commands.SendForgotPasswordEmailCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Exceptions;
using System.Web;
using Microsoft.Extensions.Logging;

public class SendForgotPasswordEmailRequestHandler : IRequestHandler<SendForgotPasswordEmailRequest>
{
    private readonly UserManager<AppUser> userManager;
    private readonly IEmailService emailService;
    private readonly ICryptoService cryptoService;
    private readonly IConfiguration configuration;
    private readonly ILogger<SendForgotPasswordEmailRequestHandler> logger;

    public SendForgotPasswordEmailRequestHandler(
        UserManager<AppUser> userManager,
        IEmailService emailService,
        ICryptoService cryptoService,
        IConfiguration configuration,
        ILogger<SendForgotPasswordEmailRequestHandler> logger)
    {
        this.userManager = userManager;
        this.emailService = emailService;
        this.cryptoService = cryptoService;
        this.configuration = configuration;
        this.logger = logger;
    }

    public async Task Handle(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("SendForgotPasswordEmailRequestHandler started handling request for email: {Email}", request.Email);

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            logger.LogError("User not found with email: {Email}", request.Email);
            throw new NotFoundException("User not found");
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var encryptedToken = cryptoService.Encrypt(token);

        user.PasswordResetToken = encryptedToken;
        user.PasswordResetTokenGeneratedAt = DateTime.UtcNow;

        await userManager.UpdateAsync(user);

        // Construct reset password URL
        var frontEndHost = configuration["FrontEnd:Host"];
        var resetUrl = $"{frontEndHost}/reset-password?email={HttpUtility.UrlEncode(request.Email)}&token={HttpUtility.UrlEncode(token)}";

        // Send email with the reset URL
        await emailService.SendEmailAsync(request.Email, "Reset Password", $"Click <a href='{resetUrl}'>here</a> to reset your password.");

        logger.LogInformation("Password reset email sent to {Email} with reset URL: {ResetUrl}", request.Email, resetUrl);
    }
}
