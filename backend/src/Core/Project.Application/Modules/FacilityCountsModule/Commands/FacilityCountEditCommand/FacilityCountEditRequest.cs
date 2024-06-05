using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountEditCommand
{
    public class FacilityCountEditRequest : IRequest<FacilityCount>
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
