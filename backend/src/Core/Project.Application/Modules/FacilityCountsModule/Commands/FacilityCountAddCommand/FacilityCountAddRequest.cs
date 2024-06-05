using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountAddCommand
{
    public class FacilityCountAddRequest: IRequest<FacilityCount>
    {
        public int PropertyId { get; set; }
        public int FacilityId { get; set; }
        public int Count { get; set; }
    }
}
