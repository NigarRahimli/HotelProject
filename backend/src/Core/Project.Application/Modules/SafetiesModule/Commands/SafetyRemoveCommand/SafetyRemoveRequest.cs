using MediatR;


namespace Project.Application.Modules.SafetiesModule.Commands.SafetyRemoveCommand
{
    public class SafetyRemoveRequest:IRequest
    {
        public int Id { get; set; }
    }
}
