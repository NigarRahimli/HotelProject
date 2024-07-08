using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Modules.AccountModule.Commands.ResetPasswordCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;

public class ResetCurrentPasswordRequestHandler : IRequestHandler<ResetCurrentPasswordRequest>
{
    private readonly UserManager<AppUser> userManager;
    private readonly ICryptoService cryptoService;

    public ResetCurrentPasswordRequestHandler(ICryptoService cryptoService, UserManager<AppUser> userManager)
    {
        this.cryptoService = cryptoService;
        this.userManager = userManager;
    }

    public async Task Handle(ResetCurrentPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new Exception("User not found");

        var decryptedToken = cryptoService.Decrypt(request.Token);
        var result = await userManager.ResetPasswordAsync(user, decryptedToken, request.NewPassword);

        if (!result.Succeeded)
            throw new Exception("Password reset failed");

    }
}
