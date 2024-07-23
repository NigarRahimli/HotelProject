using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Application.Modules.ReviewsModule.Queries.GetAveragePerCategoryQuery;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAveragePerCategoryQuery;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Handlers
{
    public class ReviewGetAveragePerCategoryRequestHandler : IRequestHandler<ReviewGetAveragePerCategoryRequest, IEnumerable<ReviewAverageDto>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IReviewCategoryRepository reviewCategoryRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<ReviewGetAveragePerCategoryRequestHandler> logger;

        public ReviewGetAveragePerCategoryRequestHandler(IReviewRepository reviewRepository, IReviewCategoryRepository reviewCategoryRepository, ILogger<ReviewGetAveragePerCategoryRequestHandler> logger, IPropertyRepository propertyRepository)
        {
            this.reviewRepository = reviewRepository;
            this.reviewCategoryRepository = reviewCategoryRepository;
            this.logger = logger;
            this.propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<ReviewAverageDto>> Handle(ReviewGetAveragePerCategoryRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReviewGetAveragePerCategoryRequest for PropertyId: {PropertyId}", request.PropertyId);
            await propertyRepository.GetAsync(x=>x.Id==request.PropertyId && x.DeletedBy == null && x.DeletedBy == null, cancellationToken);

            logger.LogInformation("Property with PropertyId: {PropertyId} found", request.PropertyId);

            var reviewCategories = await reviewCategoryRepository.GetAll().ToListAsync(cancellationToken);
            if (!reviewCategories.Any())
            {
                logger.LogWarning("No review categories found");
                return Enumerable.Empty<ReviewAverageDto>();
            }

            var reviews = await reviewRepository.GetAll(r => r.PropertyId == request.PropertyId).ToListAsync(cancellationToken);
            if (!reviews.Any())
            {
                logger.LogWarning("No reviews found for PropertyId: {PropertyId}", request.PropertyId);
                return Enumerable.Empty<ReviewAverageDto>();
            }

            var categoryAverages = reviewCategories.Select(category => new ReviewAverageDto
            {
                Id = category.Id,
                Name = category.Name,
                AverageStars = reviews.Where(r => r.CategoryId == category.Id).DefaultIfEmpty().Average(r => r?.Stars ?? 0)
            });

            logger.LogInformation("Calculated average stars per category for PropertyId: {PropertyId}", request.PropertyId);

            return categoryAverages;
        }
    }
}
