using MediatR;

namespace Project.Application.Modules.RoleModule.Commands.ManageMemberCommand
{
    public class ManageMemberRequest : IRequest
    {

        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public bool IsSelected { get; set; }
    }
}
