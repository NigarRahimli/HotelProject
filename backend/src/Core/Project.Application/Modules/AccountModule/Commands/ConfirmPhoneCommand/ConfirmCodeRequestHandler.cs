using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Modules.AccountModule.Commands.ConfirmPhoneCommand;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Project.Infrastructure.Exceptions;

public class ConfirmCodeRequestHandler : IRequestHandler<ConfirmCodeRequest>
{
    private readonly IUserRepository userRepository;
    private readonly ICryptoService cryptoService;
    private readonly IHttpContextAccessor contextAccessor;
    private readonly ILogger<ConfirmCodeRequestHandler> logger;

    public ConfirmCodeRequestHandler(
        IHttpContextAccessor contextAccessor,
        IUserRepository userRepository,
        ICryptoService cryptoService,
        ILogger<ConfirmCodeRequestHandler> logger)
    {
        this.contextAccessor = contextAccessor;
        this.userRepository = userRepository;
        this.cryptoService = cryptoService;
        this.logger = logger;
    }

    public async Task Handle(ConfirmCodeRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("ConfirmCodeRequestHandler started handling request for user");

        var userId = contextAccessor.HttpContext.GetUserIdExtension();
        var user = await userRepository.GetAsync(x => x.Id == userId);
        if (user == null)
        {
            logger.LogError("User not found with ID '{UserId}'", userId);
            throw new NotFoundException("User not found.");
        }

        if (user.PhoneConfirmationCodeGeneratedAt == null ||
            (DateTime.UtcNow - user.PhoneConfirmationCodeGeneratedAt.Value).TotalMinutes > 2)
        {
            logger.LogWarning("Confirmation code has expired for user with ID '{UserId}'", userId);
            throw new BadRequestException("Confirmation code has expired.");
        }

        var decryptedCode = cryptoService.Decrypt(user.PhoneConfirmationCode);
        if (decryptedCode != request.ConfirmationCode)
        {
            logger.LogWarning("Invalid confirmation code for user with ID '{UserId}'", userId);
            throw new BadRequestException("Invalid confirmation code.");
        }

        user.PhoneNumberConfirmed = true;
        await userRepository.SaveAsync();

        logger.LogInformation("ConfirmCodeRequestHandler successfully confirmed phone number for user with ID '{UserId}'", userId);
    }
}
