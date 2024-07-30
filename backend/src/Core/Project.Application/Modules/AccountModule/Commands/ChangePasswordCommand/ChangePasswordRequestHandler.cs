using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.AccountModule.Commands.ChangePasswordCommand
{
    public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<ChangePasswordRequestHandler> logger;

        public ChangePasswordRequestHandler(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, ILogger<ChangePasswordRequestHandler> logger)
        {
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
            this.logger = logger;
        }

        public async Task Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("ChangePasswordRequestHandler started handling request for user");

            var userIdInt = contextAccessor.HttpContext.GetUserIdExtension();
            if (userIdInt == 0)
            {
                logger.LogError("Invalid user ID");
                throw new ApplicationException("Invalid user ID.");
            }

            var userId = userIdInt.ToString();

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                logger.LogError("User not found with ID '{UserId}'", userId);
                throw new NotFoundException($"User not found.");
            }

            var hasher = new PasswordHasher<AppUser>();
            var passwordVerificationResult = hasher.VerifyHashedPassword(user, user.PasswordHash, request.CurrentPassword);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                logger.LogError("Current password is incorrect for user with ID '{UserId}'", userId);
                throw new BadRequestException("Please enter the current password correctly.");
            }

            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                logger.LogError("Failed to change password for user with ID '{UserId}'", userId);
                throw new ApplicationException("Failed to change password for user.");
            }

            logger.LogInformation("ChangePasswordRequestHandler successfully changed password for user with ID '{UserId}'", userId);
        }
    }
}
