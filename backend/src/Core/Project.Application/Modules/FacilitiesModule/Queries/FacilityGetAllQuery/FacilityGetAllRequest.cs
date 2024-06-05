using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetAllQuery
{
    public class FacilityGetAllRequest:IRequest<IEnumerable<Facility>>
    {
    }
}
