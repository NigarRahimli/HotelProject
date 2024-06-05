using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetByIdQuery
{
    public class PropertyGetByIdRequest:IRequest<Property>
    {
        public int Id { get; set; }
    }
}
