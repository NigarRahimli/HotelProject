using MediatR;
using Microsoft.AspNetCore.Http;

namespace Project.Application.Modules.AccountModule.Commands.UploadProfilePhotoCommand
{
    public class UploadProfilePhotoRequest:IRequest
    {
        public IFormFile ProfileImg { get; set; }
    }
}
