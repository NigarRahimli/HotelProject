using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;


namespace Project.Repository
{
    public class ReviewRepository : AsyncRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DbContext db) : base(db)
        {
        }
    }
}
