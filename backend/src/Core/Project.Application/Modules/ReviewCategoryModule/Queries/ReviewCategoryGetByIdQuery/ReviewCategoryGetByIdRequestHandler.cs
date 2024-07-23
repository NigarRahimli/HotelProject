using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetByIdQuery
{
    class ReviewCategoryGetByIdRequestHandler : IRequestHandler<ReviewCategoryGetByIdRequest, ReviewCategory>
    {
        private readonly IReviewCategoryRepository reviewCategoryRepository;
        private readonly ILogger<ReviewCategoryGetByIdRequestHandler> logger;

        public ReviewCategoryGetByIdRequestHandler(IReviewCategoryRepository reviewCategoryRepository, ILogger<ReviewCategoryGetByIdRequestHandler> logger)
        {
            this.reviewCategoryRepository = reviewCategoryRepository;
            this.logger = logger;
        }

        public async Task<ReviewCategory> Handle(ReviewCategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReviewCategoryGetByIdRequest for Id: {Id}", request.Id);

            var entity = await reviewCategoryRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
        
                logger.LogInformation("ReviewCategory with Id: {Id} retrieved successfully", request.Id);
           
            return entity;
        }
    }
}
