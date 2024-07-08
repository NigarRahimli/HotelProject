using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Project.Application.Modules.AccountModule.Commands.SendForgotPasswordEmailCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using System.Web;

public class SendForgotPasswordEmailRequestHandler : IRequestHandler<SendForgotPasswordEmailRequest>
{
    private readonly UserManager<AppUser> userManager;
    private readonly IEmailService emailService;
    private readonly ICryptoService cryptoService;
    private readonly IConfiguration configuration;

    public SendForgotPasswordEmailRequestHandler(UserManager<AppUser> userManager, IEmailService emailService, ICryptoService cryptoService, IConfiguration configuration)
    {
        this.userManager = userManager;
       this.emailService = emailService;
        this.cryptoService = cryptoService;
        this.configuration = configuration;
    }

    public async Task Handle(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Generate password reset token
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        // Encrypt the token before storing it (assuming ICryptoService handles encryption securely)
        var encryptedToken = cryptoService.Encrypt(token);

        // Store encrypted token and timestamp in user object
        user.PasswordResetToken = encryptedToken;
        user.PasswordResetTokenGeneratedAt = DateTime.UtcNow;

        // Update user with new token and timestamp
        await userManager.UpdateAsync(user);

        // Construct reset password URL
        var frontEndHost = configuration["FrontEnd:Host"];
        var resetUrl = $"{frontEndHost}/reset-password?email={HttpUtility.UrlEncode(request.Email)}&token={HttpUtility.UrlEncode(token)}";

        // Send email with the reset URL
        await emailService.SendEmailAsync(request.Email, "Reset Password", $"Click <a href='{resetUrl}'>here</a> to reset your password.");

    }
}
