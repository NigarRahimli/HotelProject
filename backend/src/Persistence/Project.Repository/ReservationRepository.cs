using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
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
            // Query reservations that overlap with the specified check-in and check-out times

            var overlappingReservations = await db.Set<Reservation>()
                .Where(r =>
                    ((r.CheckInTime < checkOutTime && r.CheckOutTime > checkInTime) // Check for overlap
                    || (r.CheckInTime == checkInTime && r.CheckOutTime == checkOutTime))&&r.IsApproved // Check exact match
                )
                .ToListAsync();

            return overlappingReservations;
        }
    }
}
