using MediatR;
using Project.Application.Repositories;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryRemoveCommand
{
    class ReviewCategoryRemoveRequestHandler : IRequestHandler<ReviewCategoryRemoveRequest>
    {
        private readonly IReviewCategoryRepository ReviewCategoryRepository;

        public ReviewCategoryRemoveRequestHandler(IReviewCategoryRepository ReviewCategoryRepository)
        {
            this.ReviewCategoryRepository = ReviewCategoryRepository;
        }
        public async Task Handle(ReviewCategoryRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await ReviewCategoryRepository.GetAsync(x=>x.Id==request.Id && x.DeletedBy==null,cancellationToken);
            ReviewCategoryRepository.Remove(entity);
            await ReviewCategoryRepository.SaveAsync(cancellationToken);
        }
    }
}
