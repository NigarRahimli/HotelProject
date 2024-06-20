using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery
{
    public class PropertyGetAllNearbyRequestHandler: IRequestHandler<PropertyGetAllNearbyRequest, IEnumerable<PropertyNearbyDto>>
    {
        private readonly IPropertyRepository propertyRepository;

        public PropertyGetAllNearbyRequestHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<PropertyNearbyDto>> Handle(PropertyGetAllNearbyRequest request, CancellationToken cancellationToken)
        {
           return await propertyRepository.GetNearbyPropertiesAsync(request.UserLocation).Take(request.Number).ToListAsync(cancellationToken) ;
        }
    }
}
