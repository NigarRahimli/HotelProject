using MediatR;
using Project.Domain.Models.Entities.Membership;


namespace Project.Application.Modules.RoleModule.Commands.RoleAddCommand
{
    public class RoleAddRequest : IRequest<AppRole>
    {
        public string Name { get; set; }
    }
}
