using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;
using Project.Infrastructure.Concretes;


namespace Project.Repository
{
    public class ReservationRepository : AsyncRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Reservation>> GetOverlappingReservationsAsync(DateTime checkInTime, DateTime checkOutTime)
        {
            
            var overlappingReservations = await db.Set<Reservation>()
                .Where(r =>
                    ((r.CheckInTime < checkOutTime && r.CheckOutTime > checkInTime) // Check for overlap
                    || (r.CheckInTime == checkInTime && r.CheckOutTime == checkOutTime))&& r.ReservationStatus== ReservationStatus.Approved // Check exact match
                )
                .ToListAsync();

            return overlappingReservations;
        }

        public async Task<bool> IsReservationTimeFrameAvailable(int propertyId, DateTime checkInTime, DateTime checkOutTime,CancellationToken cancellationToken)
        {
            return !await db.Set<Reservation>()
                .AnyAsync(r => r.PropertyId == propertyId
                    && r.ReservationStatus == ReservationStatus.Approved
                    && ((checkInTime >= r.CheckInTime && checkInTime < r.CheckOutTime)
                        || (checkOutTime > r.CheckInTime && checkOutTime <= r.CheckOutTime)
                        || (checkInTime < r.CheckInTime && checkOutTime > r.CheckOutTime)),cancellationToken);
        }
    }
}
