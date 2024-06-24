using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;


namespace Project.Application.Repositories
{
    public interface IReviewCategoryRepository : IAsyncRepository<ReviewCategory>
    {
    }
}
