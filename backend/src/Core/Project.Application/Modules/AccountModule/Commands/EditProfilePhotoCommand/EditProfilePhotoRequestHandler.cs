using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace Project.Application.Modules.AccountModule.Commands.EditProfilePhotoCommand
{
    public class EditProfileRequestHandler : IRequestHandler<EditProfilePhotoRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserRepository userRepository;
        private readonly IFileService fileService;
        private readonly ILogger<EditProfileRequestHandler> logger;

        public EditProfileRequestHandler(
            IHttpContextAccessor contextAccessor,
            IUserRepository userRepository,
            IFileService fileService,
            ILogger<EditProfileRequestHandler> logger)
        {
            this.contextAccessor = contextAccessor;
            this.userRepository = userRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task Handle(EditProfilePhotoRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("EditProfileRequestHandler started handling request for user profile photo update");

            var userId = contextAccessor.HttpContext!.GetUserIdExtension();
            logger.LogDebug("Retrieved user ID: {UserId}", userId);

            var user = await userRepository.GetAsync(x => x.Id == userId);
         

            logger.LogDebug("User found with ID: {UserId}. Current profile image URL: {ProfileImgUrl}", userId, user.ProfileImgUrl);

            var profileImage = await fileService.ChangeSingleFileAsync(user.ProfileImgUrl, request.ProfileImg, "profile");
            logger.LogDebug("Profile image updated for user ID: {UserId}. New profile image URL: {NewProfileImgUrl}", userId, profileImage.Url);

            user.ProfileImgUrl = profileImage.Url;
            await userRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Profile photo updated successfully for user ID: {UserId}", userId);
        }
    }
}
