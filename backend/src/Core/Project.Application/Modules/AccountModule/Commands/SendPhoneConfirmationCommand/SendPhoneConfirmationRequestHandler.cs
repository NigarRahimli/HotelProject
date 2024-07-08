using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Application.Services;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.SendPhoneConfirmationCommand
{
    public class SendPhoneConfirmationRequestHandler : IRequestHandler<SendPhoneConfirmationRequest>
    {
        private readonly IUserRepository userRepository;
        private readonly ISMSService smsService;
        private readonly ICryptoService cryptoService;
        private readonly IHttpContextAccessor contextAccessor;
        public SendPhoneConfirmationRequestHandler(IUserRepository userRepository, ISMSService smsService, ICryptoService cryptoService, IHttpContextAccessor contextAccessor)
        {
            this.userRepository = userRepository;
            this.smsService = smsService;
            this.cryptoService = cryptoService;
            this.contextAccessor = contextAccessor;
        }


        public async Task Handle(SendPhoneConfirmationRequest request, CancellationToken cancellationToken)
        {
            var userId=contextAccessor.HttpContext.GetUserIdExtension();
            var user = await userRepository.GetAsync(x=>x.Id==userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var confirmationCode = new Random().Next(1000, 9999).ToString();
            user.PhoneConfirmationCode = cryptoService.Encrypt(confirmationCode);
            user.PhoneNumber = request.PhoneNumber;
            user.PhoneConfirmationCodeGeneratedAt = DateTime.UtcNow; 

            await userRepository.SaveAsync(cancellationToken);

            await smsService.SendSmsAsync(user.PhoneNumber, $"Your confirmation code is {confirmationCode}");
        }
    }
}
