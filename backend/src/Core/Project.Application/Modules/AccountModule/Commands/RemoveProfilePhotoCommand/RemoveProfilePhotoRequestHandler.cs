using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.AccountModule.Commands.RemoveProfilePhotoCommand
{
    public class RemoveProfileRequestHandler : IRequestHandler<RemoveProfilePhotoRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserRepository userRepository;


        public RemoveProfileRequestHandler(IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            this.contextAccessor = contextAccessor;
            this.userRepository = userRepository;
           
        }

        public async Task Handle(RemoveProfilePhotoRequest request, CancellationToken cancellationToken)
        {
        
            var userId=contextAccessor.HttpContext!.GetUserIdExtension();
            var user = await userRepository.GetAsync(x => x.Id == userId);
            user.ProfileImgUrl = "default/profile_avatar.png";
            await userRepository.SaveAsync(cancellationToken);
        }
    }
}
