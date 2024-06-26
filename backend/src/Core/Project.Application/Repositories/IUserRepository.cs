using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<IEnumerable<int>> GetLikedPropertyIdsAsync(CancellationToken cancellationToken);
        Task<bool> IsPropertyLikedByUserAsync(int propertyId, CancellationToken cancellationToken);
    }
}
