using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllFeatured
{
    public class PropertyGetAllFeaturedRequest : IRequest<IEnumerable<PropertyFeaturedDto>>
    {
        public int Take { get; set; }
    }
}
