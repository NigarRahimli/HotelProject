using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;


namespace Project.Repository
{
    class SafetyRepository : AsyncRepository<Safety>, ISafetyRepository
    {
        public SafetyRepository(DbContext db) : base(db)
        {
        }
    }
}
