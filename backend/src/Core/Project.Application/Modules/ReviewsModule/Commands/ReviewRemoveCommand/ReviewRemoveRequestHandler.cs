using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewModule.Commands.ReviewRemoveCommand
{
    class ReviewRemoveRequestHandler : IRequestHandler<ReviewRemoveRequest>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<ReviewRemoveRequestHandler> logger;

        public ReviewRemoveRequestHandler(IReviewRepository reviewRepository, IPropertyRepository propertyRepository, ILogger<ReviewRemoveRequestHandler> logger)
        {
            this.reviewRepository = reviewRepository;
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task Handle(ReviewRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReviewRemoveRequest for ReviewId: {ReviewId}", request.Id);

            var entity = await reviewRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            
            logger.LogWarning("Review with Id: {ReviewId} found ", request.Id);
            
            reviewRepository.Remove(entity);
            await reviewRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Review with Id: {ReviewId} removed successfully", request.Id);

            var averageReview = await reviewRepository.GetAverageReview(entity.PropertyId, cancellationToken);
            var property = await propertyRepository.GetAsync(x => x.Id == entity.PropertyId);
            property.Rate = averageReview;
            await propertyRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Property rate updated successfully for PropertyId: {PropertyId} with new Rate: {Rate}", entity.PropertyId, property.Rate);
        }
    }
}
