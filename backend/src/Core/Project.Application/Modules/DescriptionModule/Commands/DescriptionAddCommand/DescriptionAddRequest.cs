using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionsModule.Commands.DescriptionAddCommand
{
    public class DescriptionAddRequest : IRequest<Description>
    {
        public string Name { get; set; }
        public string? Explanation { get; set; }
    }
}
