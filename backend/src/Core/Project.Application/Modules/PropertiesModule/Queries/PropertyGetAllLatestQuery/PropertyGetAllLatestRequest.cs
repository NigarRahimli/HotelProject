using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllLatestQuery
{
    public class PropertyGetAllLatestRequest : IRequest<IEnumerable<PropertyWithHeartDto>>
    {
        public int Take { get; set; }
        public int UserId { get; set; }
    }
}
