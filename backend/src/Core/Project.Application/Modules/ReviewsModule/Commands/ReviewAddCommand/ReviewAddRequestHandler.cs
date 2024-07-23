using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Exceptions;
using Project.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ReviewAddRequestHandler : IRequestHandler<ReviewAddRequest, Review>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IReviewRepository reviewRepository;
    private readonly IPropertyRepository propertyRepository;
    private readonly IReviewCategoryRepository reviewCategoryRepository;
    private readonly ILogger<ReviewAddRequestHandler> logger;

    public ReviewAddRequestHandler(
        IHttpContextAccessor httpContextAccessor,
        IReviewRepository reviewRepository,
        IPropertyRepository propertyRepository,
        IReviewCategoryRepository reviewCategoryRepository,
        ILogger<ReviewAddRequestHandler> logger)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.reviewRepository = reviewRepository;
        this.propertyRepository = propertyRepository;
        this.reviewCategoryRepository = reviewCategoryRepository;
        this.logger = logger;
    }

    public async Task<Review> Handle(ReviewAddRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling ReviewAddRequest for PropertyId: {PropertyId}, CategoryId: {CategoryId}", request.PropertyId, request.CategoryId);

        await propertyRepository.GetAsync(x => x.Id == request.PropertyId && x.DeletedBy == null, cancellationToken);
        logger.LogInformation("Property with {propertyId} id exists", request.PropertyId);

        await reviewCategoryRepository.GetAsync(x => x.Id == request.CategoryId && x.DeletedBy == null, cancellationToken);
        logger.LogInformation("Review category with {categoryId} id exists", request.CategoryId);


        var userId = httpContextAccessor.HttpContext.GetUserIdExtension();
        logger.LogInformation("User founded", userId);

        var existingReview = reviewRepository.GetAll(r =>
            r.CreatedBy == userId &&
            r.PropertyId == request.PropertyId &&
            r.CategoryId == request.CategoryId).FirstOrDefault();

        if (existingReview != null)
        {
            logger.LogWarning("Review already exists for UserId: {UserId}, PropertyId: {PropertyId}, CategoryId: {CategoryId}", userId, request.PropertyId, request.CategoryId);
            throw new EntityAlreadyExistsException(nameof(Review), $"this {nameof(ReviewCategory)} and {nameof(Property)}");
        } 

            var review = new Review
        {
            PropertyId = request.PropertyId,
            CategoryId = request.CategoryId,
            Stars = request.Stars
        };

        await reviewRepository.AddAsync(review);
        await reviewRepository.SaveAsync();

        logger.LogInformation("Review added successfully for PropertyId: {PropertyId}, CategoryId: {CategoryId}", request.PropertyId, request.CategoryId);

        var averageReview = await reviewRepository.GetAverageReview(request.PropertyId, cancellationToken);
        var property = await propertyRepository.GetAsync(x => x.Id == request.PropertyId);
        property.Rate = averageReview;
        await propertyRepository.SaveAsync(cancellationToken);

        logger.LogInformation("Property rate updated successfully for PropertyId: {PropertyId} with new Rate: {Rate}", request.PropertyId, property.Rate);

        return review;
    }
}
