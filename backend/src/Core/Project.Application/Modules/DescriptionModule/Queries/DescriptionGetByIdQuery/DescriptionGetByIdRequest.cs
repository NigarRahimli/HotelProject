using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetByIdQuery
{
    public class DescriptionGetByIdRequest:IRequest<Description>
    {
        public int Id { get; set; }
    }
}
