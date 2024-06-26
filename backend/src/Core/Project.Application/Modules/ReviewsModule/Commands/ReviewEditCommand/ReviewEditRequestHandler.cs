

using MediatR;
using Project.Application.Modules.Module.Commands.EditCommand;
using Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryEditCommand;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.PropertyModule.Commands.PropertyEditCommand
{
    public class ReviewEditRequestHandler : IRequestHandler<ReviewEditRequest, Review>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IPropertyRepository propertyRepository;


        public ReviewEditRequestHandler(IReviewRepository reviewRepository, IPropertyRepository propertyRepository)
        {
            this.reviewRepository = reviewRepository;
            this.propertyRepository = propertyRepository;
        }
        public async Task<Review> Handle(ReviewEditRequest request, CancellationToken cancellationToken)
        {
            var entity = await reviewRepository.GetAsync(m => m.Id == request.Id,cancellationToken);

            entity.Stars = request.Stars;
            await reviewRepository.SaveAsync(cancellationToken);

            var averageReview = await reviewRepository.GetAverageReview(entity.PropertyId, cancellationToken);
            var property = await propertyRepository.GetAsync(x => x.Id == entity.PropertyId);
            property.Rate = averageReview;
            await propertyRepository.SaveAsync(cancellationToken);
            return entity;
        }
    }
}
