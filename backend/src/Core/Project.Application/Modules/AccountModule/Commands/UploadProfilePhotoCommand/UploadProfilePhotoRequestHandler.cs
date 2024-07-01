using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;
using Resume.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.UploadProfilePhotoCommand
{
    public class UploadProfileRequestHandler : IRequestHandler<UploadProfilePhotoRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserRepository userRepository;
        private readonly IFileService fileService;

        public UploadProfileRequestHandler(IHttpContextAccessor contextAccessor, IUserRepository userRepository, IFileService fileService)
        {
            this.contextAccessor = contextAccessor;
            this.userRepository = userRepository;
            this.fileService = fileService;
        }

        public async Task Handle(UploadProfilePhotoRequest request, CancellationToken cancellationToken)
        {
        
            var userId=contextAccessor.HttpContext!.GetUserIdExtension();
            var user = await userRepository.GetAsync(x => x.Id == userId);
            var profileImg = await fileService.UploadSingleAsync(request.ProfileImg,"profile");
            user.ProfileImgUrl =profileImg.Url;
            await userRepository.SaveAsync(cancellationToken);
            
        }
    }
}
