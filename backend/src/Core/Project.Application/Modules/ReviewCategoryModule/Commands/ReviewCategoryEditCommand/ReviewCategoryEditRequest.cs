﻿using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryEditCommand
{
    public class ReviewCategoryEditRequest : IRequest<ReviewCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
