using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;


namespace Project.Repository
{
    class AmenityRepository : AsyncRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(DbContext db) : base(db)
        {
        }
    }
}
