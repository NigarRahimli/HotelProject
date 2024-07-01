using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.LocationsModule.Queries.LocationGetAllQuery
{
    public class LocationGetAllRequest:IRequest<IEnumerable<Location>>
    {
    }
}
