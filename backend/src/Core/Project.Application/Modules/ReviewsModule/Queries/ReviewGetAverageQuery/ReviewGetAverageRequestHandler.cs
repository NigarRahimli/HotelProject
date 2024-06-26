using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAverageQuery;
using Project.Application.Repositories;

public class ReviewGetAverageRequestHandler : IRequestHandler<ReviewGetAverageRequest, double>
{
    private readonly IReviewRepository reviewRepository;

    public ReviewGetAverageRequestHandler(IReviewRepository reviewRepository)
    {
        this.reviewRepository = reviewRepository;
    }

    public async Task<double> Handle(ReviewGetAverageRequest request, CancellationToken cancellationToken)
    {
        return await reviewRepository.GetAverageReview(request.PropertyId, cancellationToken);
    }
}