using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Repository
{
    class FacilityCountRepository : AsyncRepository<FacilityCount>, IFacilityCountRepository
    {
        public FacilityCountRepository(DbContext db) : base(db)
        {
        }
    }
}