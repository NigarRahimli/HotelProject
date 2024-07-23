using MediatR;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryAddCommand
{
    public class ReviewCategoryAddRequest: IRequest<ReviewCategory>
    {
        public string Name { get; set; }
    }
}
