using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllQuery
{
    public class PropertyGetAllRequest:IRequest<IEnumerable<Property>>
    {
    }
}
