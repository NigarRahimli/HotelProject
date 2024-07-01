using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.SafetiesModule.Commands.SafetyEditCommand
{
    public class SafetyEditRequest : IRequest<Safety>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
