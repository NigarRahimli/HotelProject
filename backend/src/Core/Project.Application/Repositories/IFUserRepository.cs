using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Repositories
{
    public interface IFUserRepository : IAsyncRepository<User>
    {
        Task<IEnumerable<int>> GetLikedPropertyIdsAsync(int userId, CancellationToken cancellationToken);
        Task<bool> IsPropertyLikedByUserAsync(int userId, int propertyId, CancellationToken cancellationToken);
    }
}
