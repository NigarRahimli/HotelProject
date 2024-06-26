using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Repositories
{
    public interface IReviewRepository : IAsyncRepository<Review>
    {
    }
}
