using Project.Infrastructure.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Project.Application.Modules.AccountModule.Commands.EmailConfirmationCommand;

namespace Project.Application.Modules.AccountModule.Commands.EmailConfirmationationCommand
{
    public class EmailConfirmationRequestHandler : IRequestHandler<EmailConfirmationRequest>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ICryptoService cryptoService;

        public EmailConfirmationRequestHandler(UserManager<AppUser> userManager, ICryptoService cryptoService)
        {
            this.userManager = userManager;
            this.cryptoService = cryptoService;
        }

        public async Task Handle(EmailConfirmationRequest request, CancellationToken cancellationToken)
        {
            var decodedToken = cryptoService.Decrypt(request.Token);
            var tokenParts = decodedToken.Split('-');

            if (tokenParts.Length != 2)
            {
                throw new BadRequestException("Invalid token");
            }

            var email = tokenParts[0];
            var token = tokenParts[1];

            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                var errors = result.Errors.ToDictionary(k => k.Code, v => (IEnumerable<string>)new[] { v.Description });
                throw new BadRequestException("Email confirmation failed", errors);
            }

        }
    }
}
