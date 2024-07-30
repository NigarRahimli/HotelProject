using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace Project.Application.Modules.AccountModule.Commands.UploadProfilePhotoCommand
{
    public class UploadProfileRequestHandler : IRequestHandler<UploadProfilePhotoRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserRepository userRepository;
        private readonly IFileService fileService;
        private readonly ILogger<UploadProfileRequestHandler> logger;

        public UploadProfileRequestHandler(
            IHttpContextAccessor contextAccessor,
            IUserRepository userRepository,
            IFileService fileService,
            ILogger<UploadProfileRequestHandler> logger)
        {
            this.contextAccessor = contextAccessor;
            this.userRepository = userRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task Handle(UploadProfilePhotoRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UploadProfileRequestHandler started handling request for user profile photo upload");

            var userId = contextAccessor.HttpContext!.GetUserIdExtension();
            logger.LogDebug("Retrieved user ID: {UserId}", userId);

            var user = await userRepository.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                logger.LogError("User not found with ID '{UserId}'", userId);
                throw new Exception("User not found.");
            }

            logger.LogDebug("User found with ID: {UserId}. Current profile image URL: {ProfileImgUrl}", userId, user.ProfileImgUrl);

            var profileImg = await fileService.UploadSingleAsync(request.ProfileImg, "profile");
            logger.LogDebug("Profile image uploaded for user ID: {UserId}. New profile image URL: {NewProfileImgUrl}", userId, profileImg.Url);

            user.ProfileImgUrl = profileImg.Url;
            await userRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Profile photo uploaded successfully for user ID: {UserId}", userId);
        }
    }
}
