using MediatR;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery
{
    public class PropertyGetAllNearbyRequest : IRequest<IEnumerable<PropertyWithHeartDto>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
    }
}
