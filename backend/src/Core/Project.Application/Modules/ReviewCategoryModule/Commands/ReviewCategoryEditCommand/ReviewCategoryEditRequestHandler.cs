using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryEditCommand
{
    class ReviewCategoryEditRequestHandler : IRequestHandler<ReviewCategoryEditRequest, ReviewCategory>
    {
        private readonly IReviewCategoryRepository reviewCategoryRepository;
        private readonly ILogger<ReviewCategoryEditRequestHandler> logger;

        public ReviewCategoryEditRequestHandler(IReviewCategoryRepository reviewCategoryRepository, ILogger<ReviewCategoryEditRequestHandler> logger)
        {
            this.reviewCategoryRepository = reviewCategoryRepository;
            this.logger = logger;
        }

        public async Task<ReviewCategory> Handle(ReviewCategoryEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReviewCategoryEditRequest for Id: {Id}", request.Id);

            var entity = await reviewCategoryRepository.GetAsync(m => m.Id == request.Id && m.DeletedBy==null, cancellationToken);
            
            logger.LogWarning("ReviewCategory with Id: {Id} found", request.Id);
              
            entity.Name = request.Name;
            logger.LogInformation("Updating ReviewCategory entity with new Name: {Name}", request.Name);
            await reviewCategoryRepository.SaveAsync(cancellationToken);

            logger.LogInformation("ReviewCategory entity updated successfully");

            return entity;
        }
    }
}
