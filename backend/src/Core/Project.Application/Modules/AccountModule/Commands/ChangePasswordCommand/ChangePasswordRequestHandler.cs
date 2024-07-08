using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Extensions;


namespace Project.Application.Modules.AccountModule.Commands.ChangePasswordCommand
{
    public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public ChangePasswordRequestHandler(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }



        public async Task Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var userIdInt = contextAccessor.HttpContext.GetUserIdExtension();
            if (userIdInt == 0)
            {
                throw new ApplicationException("Invalid user ID.");
            }

            var userId = userIdInt.ToString();

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"User not found with ID '{userId}'.");
            }

            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Failed to change password for user'.");
            }
        }
    }
}
