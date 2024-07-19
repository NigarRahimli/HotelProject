using MediatR;

namespace Project.Application.Modules.KindsModule.Commands.KindRemoveCommand
{
    public class KindRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
