using MediatR;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery
{
    public class PropertyGetAllNearbyRequest : IRequest<IEnumerable<PropertyNearbyDto>>
    {
        public Point UserLocation { get; set; }
        public int Number;
    }
}
