using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetByIdQuery
{
    public class ReviewCategoryGetByIdRequest:IRequest<ReviewCategory>
    {
        public int Id { get; set; }
    }
}
