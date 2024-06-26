using MediatR;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAveragePerCategoryQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewsModule.Queries.GetAveragePerCategoryQuery
{
    public class ReviewGetAveragePerCategoryRequest : IRequest<IEnumerable<ReviewAverageDto>>
    {
        public int PropertyId { get; set; }
    }
}
