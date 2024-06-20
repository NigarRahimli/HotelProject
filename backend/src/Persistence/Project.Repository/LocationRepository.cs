using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    class LocationRepository : AsyncRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext db) : base(db)
        {
        }
    }
}
