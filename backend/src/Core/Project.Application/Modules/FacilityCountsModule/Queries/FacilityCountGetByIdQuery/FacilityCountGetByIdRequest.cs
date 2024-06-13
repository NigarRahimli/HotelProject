using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetByIdQuery
{
    public class FacilityCountGetByIdRequest:IRequest<FacilityCount>
    {
        public int Id { get; set; }
    }
}
