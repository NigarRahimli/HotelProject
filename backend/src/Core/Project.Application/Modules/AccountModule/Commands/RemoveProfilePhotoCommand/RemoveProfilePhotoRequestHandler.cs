using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.AccountModule.Commands.RemoveProfilePhotoCommand
{
    public class RemoveProfileRequestHandler : IRequestHandler<RemoveProfilePhotoRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserRepository userRepository;
        private readonly ILogger<RemoveProfileRequestHandler> logger;

        public RemoveProfileRequestHandler(
            IHttpContextAccessor contextAccessor,
            IUserRepository userRepository,
            ILogger<RemoveProfileRequestHandler> logger)
        {
            this.contextAccessor = contextAccessor;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task Handle(RemoveProfilePhotoRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("RemoveProfileRequestHandler started handling request to remove profile photo");

            var userId = contextAccessor.HttpContext!.GetUserIdExtension();
            logger.LogDebug("Retrieved user ID: {UserId}", userId);

            var user = await userRepository.GetAsync(x => x.Id == userId);


            logger.LogDebug("User found with ID: {UserId}. Current profile image URL: {ProfileImgUrl}", userId, user.ProfileImgUrl);

            if (user.ProfileImgUrl == "default/profile_avatar.png")
            {
                logger.LogWarning("Profile image URL for user ID: {UserId} is already the default", userId);
                throw new BadRequestException("Profile image is already the default.");
            }

            user.ProfileImgUrl = "default/profile_avatar.png";
            await userRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Profile photo removed successfully for user ID: {UserId}", userId);
        }
    }
}
