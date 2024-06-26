using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Application.Modules.ReviewsModule.Queries.GetAveragePerCategoryQuery;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAveragePerCategoryQuery;

namespace Project.Application.Handlers
{
    public class ReviewGetAveragePerCategoryRequestHandler : IRequestHandler<ReviewGetAveragePerCategoryRequest, IEnumerable<ReviewAverageDto>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IReviewCategoryRepository reviewCategoryRepository;

        public ReviewGetAveragePerCategoryRequestHandler(IReviewRepository reviewRepository, IReviewCategoryRepository reviewCategoryRepository)
        {
            this.reviewRepository = reviewRepository;
            this.reviewCategoryRepository = reviewCategoryRepository;
        }

        public async Task<IEnumerable<ReviewAverageDto>> Handle(ReviewGetAveragePerCategoryRequest request, CancellationToken cancellationToken)
        {
            var reviewCategories = await reviewCategoryRepository.GetAll().ToListAsync(cancellationToken);
            var reviews = await reviewRepository.GetAll(r => r.PropertyId == request.PropertyId).ToListAsync(cancellationToken);

            var categoryAverages = reviewCategories.Select(category => new ReviewAverageDto
            {
                Id = category.Id,
                Name = category.Name,
                AverageStars = reviews.Where(r => r.CategoryId == category.Id).DefaultIfEmpty().Average(r => r?.Stars ?? 0)
            });

            return categoryAverages;
        }
    }
}