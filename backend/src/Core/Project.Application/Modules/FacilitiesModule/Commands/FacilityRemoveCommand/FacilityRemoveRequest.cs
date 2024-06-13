using MediatR;


namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityRemoveCommand
{
    public class FacilityRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
