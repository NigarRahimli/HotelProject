using MediatR;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand
{
    public class ReviewAddRequest : IRequest<Review>
    {
        public int PropertyId { get; set; }
        public int CategoryId { get; set; }
        public int Stars { get; set; }
    }
}
