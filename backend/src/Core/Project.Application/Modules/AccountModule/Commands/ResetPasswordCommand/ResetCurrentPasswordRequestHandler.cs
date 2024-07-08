using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Modules.AccountModule.Commands.ResetPasswordCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using System.Web;

public class ResetCurrentPasswordRequestHandler : IRequestHandler<ResetCurrentPasswordRequest>
{
    private readonly UserManager<AppUser> userManager;
    private readonly ICryptoService cryptoService;

    public ResetCurrentPasswordRequestHandler(UserManager<AppUser> userManager, ICryptoService cryptoService)
    {
        this.userManager = userManager;
        this.cryptoService = cryptoService;
    }

    public async Task Handle(ResetCurrentPasswordRequest request, CancellationToken cancellationToken)
    {
        var decodedEmail = HttpUtility.UrlDecode(request.Email);
        var user = await userManager.FindByEmailAsync(decodedEmail);
        if (user == null)
        {
            throw new Exception("User not found");
        }



        if (string.IsNullOrEmpty(user.PasswordResetToken) || user.PasswordResetTokenGeneratedAt == null || DateTime.UtcNow > user.PasswordResetTokenGeneratedAt.Value.AddMinutes(30))
        {
            throw new Exception("Invalid or expired token");
        }

        
        var decryptedStoredToken = cryptoService.Decrypt(user.PasswordResetToken);
        var decodedToken = HttpUtility.UrlDecode(request.Token);
        
        if (!string.Equals(decryptedStoredToken, decodedToken))
        {
            throw new Exception("Token mismatch");
        }

    
        var result = await userManager.ResetPasswordAsync(user, decryptedStoredToken, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new Exception("Password reset failed");
        }


        user.PasswordResetToken = null;
        user.PasswordResetTokenGeneratedAt = null;
        await userManager.UpdateAsync(user);
    }
}
