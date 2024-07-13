using MediatR;
using Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Queries;

namespace Project.Application.Modules.RoleModule.Queries.RoleGetByIdQuery
{
    public class RoleGetByIdRequest : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }
}
