using MediatR;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByIdQuery
{
    public class FacilityGetByIdRequest:IRequest<Facility>
    {
        public int Id { get; set; }
    }
}
