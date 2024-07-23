using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.Module.Commands.EditCommand;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.PropertyModule.Commands.PropertyEditCommand
{
    public class ReviewEditRequestHandler : IRequestHandler<ReviewEditRequest, Review>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<ReviewEditRequestHandler> logger;

        public ReviewEditRequestHandler(IReviewRepository reviewRepository, IPropertyRepository propertyRepository, ILogger<ReviewEditRequestHandler> logger)
        {
            this.reviewRepository = reviewRepository;
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task<Review> Handle(ReviewEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReviewEditRequest for ReviewId: {ReviewId}", request.Id);

            var entity = await reviewRepository.GetAsync(m => m.Id == request.Id && m.DeletedBy == null, cancellationToken);

            logger.LogWarning("Review with Id: {ReviewId} found", request.Id);
             

            entity.Stars = request.Stars;
            await reviewRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Review with Id: {ReviewId} updated successfully", request.Id);

            var averageReview = await reviewRepository.GetAverageReview(entity.PropertyId, cancellationToken);
            var property = await propertyRepository.GetAsync(x => x.Id == entity.PropertyId);
            property.Rate = averageReview;
            await propertyRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Property rate updated successfully for PropertyId: {PropertyId} with new Rate: {Rate}", entity.PropertyId, property.Rate);

            return entity;
        }
    }
}
