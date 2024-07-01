using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.AccountModule.Commands.EditProfilePhotoCommand
{
    public class EditProfileRequestHandler : IRequestHandler<EditProfilePhotoRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserRepository userRepository;
        private readonly IFileService fileService;

        public EditProfileRequestHandler(IHttpContextAccessor contextAccessor, IUserRepository userRepository, IFileService fileService)
        {
            this.contextAccessor = contextAccessor;
            this.userRepository = userRepository;
            this.fileService = fileService;
        }

        public async Task Handle(EditProfilePhotoRequest request, CancellationToken cancellationToken)
        {
        
            var userId=contextAccessor.HttpContext!.GetUserIdExtension();
            var user = await userRepository.GetAsync(x => x.Id == userId);
            var profileImage = await fileService.ChangeSingleFileAsync(user.ProfileImgUrl, request.ProfileImg, "profile");

            user.ProfileImgUrl = profileImage.Url; 

            await userRepository.SaveAsync(cancellationToken);
            
        }
    }
}
