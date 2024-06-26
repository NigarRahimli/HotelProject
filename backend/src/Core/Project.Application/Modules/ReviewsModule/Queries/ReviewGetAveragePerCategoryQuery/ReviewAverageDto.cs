using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewsModule.Queries.ReviewGetAveragePerCategoryQuery
{
    public class ReviewAverageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AverageStars { get; set; }
    }
}
