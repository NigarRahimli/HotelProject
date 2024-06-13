using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionEditCommand
{
    public class DescriptionEditRequest : IRequest<Description>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Explanation { get; set; }
    }
}
