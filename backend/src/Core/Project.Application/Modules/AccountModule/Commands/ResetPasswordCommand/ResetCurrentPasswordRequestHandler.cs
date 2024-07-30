using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.AccountModule.Commands.ResetPasswordCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Exceptions;
using System.Web;

public class ResetCurrentPasswordRequestHandler : IRequestHandler<ResetCurrentPasswordRequest>
{
    private readonly UserManager<AppUser> userManager;
    private readonly ICryptoService cryptoService;
    private readonly ILogger<ResetCurrentPasswordRequestHandler> logger;

    public ResetCurrentPasswordRequestHandler(UserManager<AppUser> userManager, ICryptoService cryptoService, ILogger<ResetCurrentPasswordRequestHandler> logger)
    {
        this.userManager = userManager;
        this.cryptoService = cryptoService;
        this.logger = logger;
    }

    public async Task Handle(ResetCurrentPasswordRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling password reset request for email: {Email}", request.Email);

        var decodedEmail = HttpUtility.UrlDecode(request.Email);
        var user = await userManager.FindByEmailAsync(decodedEmail);
        if (user == null)
        {
            logger.LogWarning("User not found for email: {Email}", decodedEmail);
            throw new NotFoundException("User not found");
        }

        if (string.IsNullOrEmpty(user.PasswordResetToken) || user.PasswordResetTokenGeneratedAt == null || DateTime.UtcNow > user.PasswordResetTokenGeneratedAt.Value.AddMinutes(30))
        {
            logger.LogWarning("Invalid or expired token for user: {Email}", decodedEmail);
            throw new BadRequestException("Invalid or expired token");
        }

        var decryptedStoredToken = cryptoService.Decrypt(user.PasswordResetToken);
        var decodedToken = HttpUtility.UrlDecode(request.Token);

        if (!string.Equals(decryptedStoredToken, decodedToken))
        {
            logger.LogWarning("Token mismatch for user: {Email}", decodedEmail);
            throw new BadRequestException("Token mismatch");
        }

        var result = await userManager.ResetPasswordAsync(user, decryptedStoredToken, request.NewPassword);
        if (!result.Succeeded)
        {
            logger.LogError("Password reset failed for user: {Email}", decodedEmail);
            throw new OperationFailedException("Password reset failed");
        }

        user.PasswordResetToken = null;
        user.PasswordResetTokenGeneratedAt = null;
        await userManager.UpdateAsync(user);

        logger.LogInformation("Password reset successful for user: {Email}", decodedEmail);

    }
}
