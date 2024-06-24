using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetAllQuery
{
    class ReviewCategoryGetAllRequestHandler : IRequestHandler<ReviewCategoryGetAllRequest, IEnumerable<ReviewCategory>>
    {
        private readonly IReviewCategoryRepository ReviewCategoryRepository;

        public ReviewCategoryGetAllRequestHandler(IReviewCategoryRepository ReviewCategoryRepository)
        {
            this.ReviewCategoryRepository = ReviewCategoryRepository;
        }
        public async Task<IEnumerable<ReviewCategory>> Handle(ReviewCategoryGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await ReviewCategoryRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
