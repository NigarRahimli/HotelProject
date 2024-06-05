using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Repository
{
    public class PropertyRepository : AsyncRepository<Kind>, IKindRepository
    {
        public PropertyRepository(DbContext db) : base(db)
        {
        }
    }
}
