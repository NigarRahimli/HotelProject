using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Concretes;
using Project.Infrastructure.Extensions;

namespace Project.Repository
{
    class UserRepository : AsyncRepository<AppUser>, IUserRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        public UserRepository(DbContext db, IHttpContextAccessor contextAccessor) : base(db)
        {
            this.contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<int>> GetLikedPropertyIdsAsync(CancellationToken cancellationToken)
        {
            var userId = contextAccessor.HttpContext.GetUserIdExtension();
            return await db.Set<Like>()
              .Where(like => like.UserId == userId )
              .Select(like => like.PropertyId)
              .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsPropertyLikedByUserAsync(int propertyId, CancellationToken cancellationToken)
        {
            var userId = contextAccessor.HttpContext.GetUserIdExtension();
            return await db.Set<Like>()
                    .AnyAsync(ul => ul.UserId == userId && ul.PropertyId == propertyId, cancellationToken);
        }
    }
}

