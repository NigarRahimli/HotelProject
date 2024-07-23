using MediatR;
using Project.Application.Modules.ReviewCategoryModule.Queries.ReviewCategoryGetAllQuery;


namespace Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetAllQuery
{
    public class ReviewCategoryGetAllRequest:IRequest<IEnumerable<ReviewCategoryDto>>
    {
    }
}
