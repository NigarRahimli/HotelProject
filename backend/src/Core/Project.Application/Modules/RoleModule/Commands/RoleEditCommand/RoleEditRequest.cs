using MediatR;
using Project.Domain.Models.Entities.Membership;

namespace Project.Application.Modules.RoleModule.Commands.RoleEditCommand
{
    public class RoleEditRequest : IRequest<AppRole>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
