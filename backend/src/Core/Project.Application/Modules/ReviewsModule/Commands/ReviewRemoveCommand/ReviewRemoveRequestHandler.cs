using MediatR;
using Project.Application.Repositories;


namespace Project.Application.Modules.ReviewModule.Commands.ReviewRemoveCommand
{
    class ReviewRemoveRequestHandler : IRequestHandler<ReviewRemoveRequest>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IPropertyRepository propertyRepository;

        public ReviewRemoveRequestHandler(IReviewRepository reviewRepository, IPropertyRepository propertyRepository)
        {
            this.reviewRepository = reviewRepository;
            this.propertyRepository = propertyRepository;
        }
        public async Task Handle(ReviewRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await reviewRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            reviewRepository.Remove(entity);
            await reviewRepository.SaveAsync(cancellationToken);
            var averageReview = await reviewRepository.GetAverageReview(entity.PropertyId, cancellationToken);
            var property = await propertyRepository.GetAsync(x => x.Id == entity.PropertyId);
            property.Rate = averageReview;
            await propertyRepository.SaveAsync(cancellationToken);
        }
    }
}
