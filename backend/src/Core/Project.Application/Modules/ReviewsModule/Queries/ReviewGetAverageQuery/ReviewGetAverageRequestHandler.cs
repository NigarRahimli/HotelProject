using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAverageQuery;
using Project.Application.Repositories;
using System.Threading;
using System.Threading.Tasks;

public class ReviewGetAverageRequestHandler : IRequestHandler<ReviewGetAverageRequest, double>
{
    private readonly IReviewRepository reviewRepository;
    private readonly IPropertyRepository propertyRepository;
    private readonly ILogger<ReviewGetAverageRequestHandler> logger;

    public ReviewGetAverageRequestHandler(IReviewRepository reviewRepository, ILogger<ReviewGetAverageRequestHandler> logger, IPropertyRepository propertyRepository)
    {
        this.reviewRepository = reviewRepository;
        this.logger = logger;
        this.propertyRepository = propertyRepository;
    }

    public async Task<double> Handle(ReviewGetAverageRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling ReviewGetAverageRequest for PropertyId: {PropertyId}", request.PropertyId);
        await propertyRepository.GetAsync(x => x.Id == request.PropertyId && x.DeletedBy == null && x.DeletedBy == null, cancellationToken);

        logger.LogInformation("Property with PropertyId: {PropertyId} found", request.PropertyId);

        var averageReview = await reviewRepository.GetAverageReview(request.PropertyId, cancellationToken);

        logger.LogInformation("Calculated average review for PropertyId: {PropertyId} is {AverageReview}", request.PropertyId, averageReview);

        return averageReview;
    }
}
