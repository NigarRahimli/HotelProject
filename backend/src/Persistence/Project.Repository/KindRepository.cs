using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;

namespace Project.Repository
{
    class KindRepository : AsyncRepository<Kind>, IKindRepository
    {
        public KindRepository(DbContext db) : base(db)
        {
        }
    }
}
