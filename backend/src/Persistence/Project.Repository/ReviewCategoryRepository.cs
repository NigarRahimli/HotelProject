using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;


namespace Project.Repository
{
    class ReviewCategoryRepository : AsyncRepository<ReviewCategory>, IReviewCategoryRepository
    {
        public ReviewCategoryRepository(DbContext db) : base(db)
        {
        }
    }
}
