using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Infrastructure.Extensions;
using Project.Domain.Models.Entities;
using Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand;



namespace Project.Application.Properties.Commands
{
    public class ReviewAddRequestHandler : IRequestHandler<ReviewAddRequest,Review>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReviewRepository ReviewRepository;

        public ReviewAddRequestHandler(IHttpContextAccessor httpContextAccessor, IReviewRepository ReviewRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.ReviewRepository = ReviewRepository;
        }

        public async Task<Review> Handle(ReviewAddRequest request, CancellationToken cancellationToken)
        {
             var userId =  httpContextAccessor.HttpContext.GetUserIdExtension();


            var existingReview = ReviewRepository.GetAll(r =>
                r.CreatedBy == userId &&
                r.PropertyId == request.PropertyId &&
                r.CategoryId == request.CategoryId).FirstOrDefault();

            if (existingReview != null)
            {
                throw new Exception("Review already exists");
            }

            var Review = new Review
            {
                PropertyId = request.PropertyId,
                CategoryId = request.CategoryId,
                Stars = request.Stars
            };

            await ReviewRepository.AddAsync(Review);
            await ReviewRepository.SaveAsync();

            return Review;
        }

    }
}
