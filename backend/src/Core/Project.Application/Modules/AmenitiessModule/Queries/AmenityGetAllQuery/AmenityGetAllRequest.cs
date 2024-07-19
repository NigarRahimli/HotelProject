using MediatR;
using Project.Application.Modules.AmenitiessModule.Queries;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Queries.AmenityGetAllQuery
{
    public class AmenityGetAllRequest:IRequest<IEnumerable<AmenityDto>>
    {
    }
}
