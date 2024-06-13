using MediatR;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetAllQuery
{
    public class FacilityCountGetAllRequest:IRequest<IEnumerable<FacilityCount>>
    {
    }
}
