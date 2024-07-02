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
    }
}
