using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyAddCommand
{
    public class SafetyAddRequest: IRequest<Safety>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }

    }
}
