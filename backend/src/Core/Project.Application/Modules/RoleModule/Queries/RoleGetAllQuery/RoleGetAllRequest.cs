using MediatR;


namespace Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Queries.RoleGetAllQuery
{
    public class RoleGetAllRequest : IRequest<IEnumerable<RoleDto>>
    {
    }
}
