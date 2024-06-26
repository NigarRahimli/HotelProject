using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Extensions;

public class ReviewAddRequestHandler : IRequestHandler<ReviewAddRequest, Review>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IReviewRepository reviewRepository;
    private readonly IPropertyRepository propertyRepository; // Add property repository

    public ReviewAddRequestHandler(
        IHttpContextAccessor httpContextAccessor,
        IReviewRepository reviewRepository,
        IPropertyRepository propertyRepository) // Inject property repository
    {
        this.httpContextAccessor = httpContextAccessor;
        this.reviewRepository = reviewRepository;
        this.propertyRepository = propertyRepository;
    }

    public async Task<Review> Handle(ReviewAddRequest request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext.GetUserIdExtension();

        var existingReview = reviewRepository.GetAll(r =>
            r.CreatedBy == userId &&
            r.PropertyId == request.PropertyId &&
            r.CategoryId == request.CategoryId).FirstOrDefault();

        if (existingReview != null)
        {
            throw new Exception("Review already exists");
        }

        var review = new Review
        {
            PropertyId = request.PropertyId,
            CategoryId = request.CategoryId,
            Stars = request.Stars
        };

        await reviewRepository.AddAsync(review);
        await reviewRepository.SaveAsync();

        var averageReview = await reviewRepository.GetAverageReview(request.PropertyId, cancellationToken);
        var property = await propertyRepository.GetAsync(x=>x.Id==request.PropertyId); 
        property.Rate = averageReview; 
        await propertyRepository.SaveAsync(cancellationToken);

        return review;
    }
}
