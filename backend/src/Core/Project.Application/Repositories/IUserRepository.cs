using Project.Domain.Models.Entities;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Repositories
{
    public interface IUserRepository : IAsyncRepository<AppUser>
    {
        Task<IEnumerable<int>> GetLikedPropertyIdsAsync(CancellationToken cancellationToken);
        Task<bool> IsPropertyLikedByUserAsync(int propertyId, CancellationToken cancellationToken);
    }
}
