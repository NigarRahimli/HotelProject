using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;


namespace Project.Repository
{
    class DescriptionRepository : AsyncRepository<Description>, IDescriptionRepository
    {
        public DescriptionRepository(DbContext db) : base(db)
        {
        }
    }
}
