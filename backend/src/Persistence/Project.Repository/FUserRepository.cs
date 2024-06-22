using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Repository
{
    class FUserRepository : AsyncRepository<User>, IFUserRepository
    {
        public FUserRepository(DbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<int>> GetLikedPropertyIdsAsync(int userId, CancellationToken cancellationToken)
        {
            return await db.Set<Like>()
              .Where(like => like.UserId == userId)
              .Select(like => like.PropertyId)
              .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsPropertyLikedByUserAsync(int userId, int propertyId, CancellationToken cancellationToken)
        {
               return await db.Set<Like>()
                    .AnyAsync(ul => ul.UserId == userId && ul.PropertyId == propertyId, cancellationToken);
            }
        }
    }

