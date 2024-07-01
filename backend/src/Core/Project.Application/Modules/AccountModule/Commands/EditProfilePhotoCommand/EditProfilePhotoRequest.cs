using MediatR;
using Microsoft.AspNetCore.Http;


namespace Project.Application.Modules.AccountModule.Commands.EditProfilePhotoCommand
{
    public class EditProfilePhotoRequest:IRequest
    {
        public IFormFile ProfileImg { get; set; }
    }
}
