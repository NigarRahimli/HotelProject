using MediatR;
using Project.Application.Modules.LocationsModule.Queries.LocationGetByUserIdQuery;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.LocationsModule.Queries.KindGetAllQuery
{
    public class LocationGetByUserIdRequest : IRequest<IEnumerable<LocationByUserDto>>
    {
    }
}
