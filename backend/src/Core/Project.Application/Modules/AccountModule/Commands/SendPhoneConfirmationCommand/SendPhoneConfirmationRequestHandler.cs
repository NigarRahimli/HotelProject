using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Exceptions;
using Project.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace Project.Application.Modules.AccountModule.Commands.SendPhoneConfirmationCommand
{
    public class SendPhoneConfirmationRequestHandler : IRequestHandler<SendPhoneConfirmationRequest>
    {
        private readonly IUserRepository userRepository;
        private readonly ISMSService smsService;
        private readonly ICryptoService cryptoService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<SendPhoneConfirmationRequestHandler> logger;

        public SendPhoneConfirmationRequestHandler(
            IUserRepository userRepository,
            ISMSService smsService,
            ICryptoService cryptoService,
            IHttpContextAccessor contextAccessor,
            ILogger<SendPhoneConfirmationRequestHandler> logger)
        {
            this.userRepository = userRepository;
            this.smsService = smsService;
            this.cryptoService = cryptoService;
            this.contextAccessor = contextAccessor;
            this.logger = logger;
        }

        public async Task Handle(SendPhoneConfirmationRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("SendPhoneConfirmationRequestHandler started handling request for user");

            var userId = contextAccessor.HttpContext.GetUserIdExtension();
            var user = await userRepository.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                logger.LogError("User not found with ID '{UserId}'", userId);
                throw new Exception("User not found.");
            }

            if (user.PhoneNumber == request.PhoneNumber && user.PhoneNumberConfirmed)
            {
                logger.LogWarning("Phone number {PhoneNumber} is already confirmed for user with ID '{UserId}'", request.PhoneNumber, userId);
                throw new BadRequestException("Phone number already confirmed.");
            }

            var existingUserWithPhoneNumber = userRepository.GetAll(x => x.PhoneNumber == request.PhoneNumber).FirstOrDefault();
            if (existingUserWithPhoneNumber != null && existingUserWithPhoneNumber.Id != userId)
            {
                logger.LogWarning("Phone number {PhoneNumber} is already used by another user with ID '{ExistingUserId}'", request.PhoneNumber, existingUserWithPhoneNumber.Id);
                throw new BadRequestException("Phone number already used by another user.");
            }

            var confirmationCode = new Random().Next(1000, 999999).ToString();
            user.PhoneConfirmationCode = cryptoService.Encrypt(confirmationCode);
            user.PhoneNumber = request.PhoneNumber;
            user.PhoneConfirmationCodeGeneratedAt = DateTime.UtcNow;

            await userRepository.SaveAsync(cancellationToken);

            await smsService.SendSmsAsync(user.PhoneNumber, $"Your confirmation code is {confirmationCode}");

            logger.LogInformation("SendPhoneConfirmationRequestHandler successfully sent confirmation code to user with ID '{UserId}'", userId);
        }
    }
}
