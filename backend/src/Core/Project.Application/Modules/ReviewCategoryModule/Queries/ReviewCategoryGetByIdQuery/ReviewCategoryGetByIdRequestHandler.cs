using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetByIdQuery
{
    class ReviewCategoryGetByIdRequestHandler : IRequestHandler<ReviewCategoryGetByIdRequest, ReviewCategory>
    {
        private readonly IReviewCategoryRepository ReviewCategoryRepository;

        public ReviewCategoryGetByIdRequestHandler(IReviewCategoryRepository ReviewCategoryRepository)
        {
            this.ReviewCategoryRepository = ReviewCategoryRepository;
        }
        public async Task<ReviewCategory> Handle(ReviewCategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await ReviewCategoryRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
