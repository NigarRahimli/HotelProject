using MediatR;

using Property = Project.Domain.Models.Entities.Property;

namespace Project.Application.Modules.Module.Commands.EditCommand
{
    public class PropertyEditRequest : IRequest<Property>
    {
        public int Id { get; set; }
        public int Stars { get; set; }
    }
}
