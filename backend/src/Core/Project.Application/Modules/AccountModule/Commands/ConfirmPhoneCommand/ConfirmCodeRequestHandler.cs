using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Modules.AccountModule.Commands.ConfirmPhoneCommand;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;

public class ConfirmCodeRequestHandler : IRequestHandler<ConfirmCodeRequest>
{
    private readonly IUserRepository userRepository;
    private readonly ICryptoService cryptoService;
    private readonly IHttpContextAccessor contextAccessor;

    public ConfirmCodeRequestHandler(IHttpContextAccessor contextAccessor, IUserRepository userRepository, ICryptoService cryptoService)
    {

        this.contextAccessor = contextAccessor;
        this.userRepository = userRepository;
        this.cryptoService = cryptoService;
    }

    public async Task Handle(ConfirmCodeRequest request, CancellationToken cancellationToken)
    {
        var userId = contextAccessor.HttpContext.GetUserIdExtension();
        var user = await userRepository.GetAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
           
        // Check if the confirmation code has expired
        if (user.PhoneConfirmationCodeGeneratedAt == null ||
            (DateTime.UtcNow - user.PhoneConfirmationCodeGeneratedAt.Value).TotalMinutes > 2)
        {
            throw new Exception("Confirmation code has expired.");
        }

        var decryptedCode = cryptoService.Decrypt(user.PhoneConfirmationCode);
        if (decryptedCode != request.ConfirmationCode)
        {
            throw new Exception("Invalid confirmation code.");
        }

        user.PhoneNumberConfirmed = true;
        await userRepository.SaveAsync();

    }
}
