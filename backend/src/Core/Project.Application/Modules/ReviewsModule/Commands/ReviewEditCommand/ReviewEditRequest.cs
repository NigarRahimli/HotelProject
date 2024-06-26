using MediatR;
using Project.Domain.Models.Entities;
using Property = Project.Domain.Models.Entities.Property;

namespace Project.Application.Modules.Module.Commands.EditCommand
{
    public class ReviewEditRequest : IRequest<Review>
    {
        public int Id { get; set; }
        public int Stars { get; set; }
    }
}
