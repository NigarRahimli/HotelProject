using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.ReviewCategoryModule.Queries.ReviewCategoryGetAllQuery;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetAllQuery
{
    class ReviewCategoryGetAllRequestHandler : IRequestHandler<ReviewCategoryGetAllRequest, IEnumerable<ReviewCategoryDto>>
    {
        private readonly IReviewCategoryRepository reviewCategoryRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ReviewCategoryGetAllRequestHandler> logger;

        public ReviewCategoryGetAllRequestHandler(IReviewCategoryRepository reviewCategoryRepository, IMapper mapper, ILogger<ReviewCategoryGetAllRequestHandler> logger)
        {
            this.reviewCategoryRepository = reviewCategoryRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<ReviewCategoryDto>> Handle(ReviewCategoryGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ReviewCategoryGetAllRequest");

            var entities = await reviewCategoryRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} review categories from the repository", entities.Count);

            var dtos = mapper.Map<List<ReviewCategoryDto>>(entities);
            logger.LogInformation("Mapped review categories to DTOs");

            return dtos;
        }
    }
}
