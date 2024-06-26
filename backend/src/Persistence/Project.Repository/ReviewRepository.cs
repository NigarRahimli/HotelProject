using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery;
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

        public async Task<double> GetAverageReview(int propertyId, CancellationToken cancellationToken)
        {
           var reviews =await db.Set<Review>().Where(r=>r.PropertyId == propertyId && r.DeletedBy==null).ToListAsync(cancellationToken);
            if (!reviews.Any())
                return 0;

            return reviews.Average(r => r.Stars);
        }
    }
}
