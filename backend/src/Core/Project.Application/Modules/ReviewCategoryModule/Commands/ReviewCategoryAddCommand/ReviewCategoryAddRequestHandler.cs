using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryAddCommand
{
    class ReviewCategoryAddRequestHandler : IRequestHandler<ReviewCategoryAddRequest, ReviewCategory>
    {
        private readonly IReviewCategoryRepository ReviewCategoryRepository;

        public ReviewCategoryAddRequestHandler(IReviewCategoryRepository ReviewCategoryRepository)
        {
            this.ReviewCategoryRepository = ReviewCategoryRepository;
        }
        public async Task<ReviewCategory> Handle(ReviewCategoryAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new ReviewCategory
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await ReviewCategoryRepository.AddAsync(entity, cancellationToken);
            await ReviewCategoryRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
