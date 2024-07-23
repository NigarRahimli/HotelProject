using MediatR;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAveragePerCategoryQuery;

namespace Project.Application.Modules.ReviewsModule.Queries.GetAveragePerCategoryQuery
{
    public class ReviewGetAveragePerCategoryRequest : IRequest<IEnumerable<ReviewAverageDto>>
    {
        public int PropertyId { get; set; }
    }
}
