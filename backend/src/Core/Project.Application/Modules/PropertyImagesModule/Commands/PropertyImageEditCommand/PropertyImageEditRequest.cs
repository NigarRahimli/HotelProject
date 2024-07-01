using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageEditCommand
{
    public class PropertyImageEditRequest : IRequest<IEnumerable<PropertyImage>>
    {
        public int PropertyId { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
