using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryAddCommand
{
    public class ReviewCategoryAddRequestHandler : IRequestHandler<ReviewCategoryAddRequest, ReviewCategory>
    {
        private readonly IReviewCategoryRepository reviewCategoryRepository;
        private readonly ILogger<ReviewCategoryAddRequestHandler> logger;

        public ReviewCategoryAddRequestHandler(IReviewCategoryRepository reviewCategoryRepository, ILogger<ReviewCategoryAddRequestHandler> logger)
        {
            this.reviewCategoryRepository = reviewCategoryRepository;
            this.logger = logger;
        }

        public async Task<ReviewCategory> Handle(ReviewCategoryAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReviewCategoryAddRequest for Name: {Name}", request.Name);

            var entity = new ReviewCategory
            {
                Name = request.Name,
            };

            logger.LogInformation("Adding new ReviewCategory entity to the repository");
            await reviewCategoryRepository.AddAsync(entity, cancellationToken);
            await reviewCategoryRepository.SaveAsync(cancellationToken);

            logger.LogInformation("New ReviewCategory entity saved successfully");

            return entity;
        }
    }
}
