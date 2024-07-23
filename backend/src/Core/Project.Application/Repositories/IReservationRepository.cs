using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Repositories
{
    public interface IReservationRepository : IAsyncRepository<Reservation>
    {
        Task<bool> IsReservationTimeFrameAvailable(int propertyId, DateTime checkInTime, DateTime checkOutTime, CancellationToken cancellationToken);
        Task<IEnumerable<Reservation>> GetOverlappingReservationsAsync(DateTime checkInTime, DateTime checkOutTime);
    }
}