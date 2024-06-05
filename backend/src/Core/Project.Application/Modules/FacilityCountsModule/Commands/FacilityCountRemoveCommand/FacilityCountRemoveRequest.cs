using MediatR;


namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountRemoveCommand
{
    public class FacilityCountRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
