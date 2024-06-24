using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryEditCommand
{
    class ReviewCategoryEditRequestHandler : IRequestHandler<ReviewCategoryEditRequest, ReviewCategory>
    {
        private readonly IReviewCategoryRepository ReviewCategoryRepository;

        public ReviewCategoryEditRequestHandler(IReviewCategoryRepository ReviewCategoryRepository)
        {
            this.ReviewCategoryRepository = ReviewCategoryRepository;
        }
        public async Task<ReviewCategory> Handle(ReviewCategoryEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await ReviewCategoryRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            await ReviewCategoryRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
