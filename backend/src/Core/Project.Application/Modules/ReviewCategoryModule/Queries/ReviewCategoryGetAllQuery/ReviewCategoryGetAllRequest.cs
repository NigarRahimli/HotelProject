﻿using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetAllQuery
{
    public class ReviewCategoryGetAllRequest:IRequest<IEnumerable<ReviewCategory>>
    {
    }
}
