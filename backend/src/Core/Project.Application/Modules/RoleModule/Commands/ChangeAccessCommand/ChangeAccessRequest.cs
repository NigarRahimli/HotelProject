using MediatR;

namespace Project.Application.Modules.RoleModule.Commands.ChangeAccessCommand
{
    public class ChangeAccessRequest : IRequest
    {
        public string PolicyName { get; set; }
        public int RoleId { get; set; }
        public bool IsSelected { get; set; }
        public string[] Policies { get; set; }
        public ChangeAccessRequest()
        {
            Policies = new string[0];
        }
    }
}
