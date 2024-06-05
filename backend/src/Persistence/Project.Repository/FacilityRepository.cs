using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Repository
{
     class FacilityRepository : AsyncRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(DbContext db) : base(db)
        {
        }
    }
}
