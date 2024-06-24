using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryRemoveCommand
{
    public class ReviewCategoryRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
