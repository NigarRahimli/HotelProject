using AutoMapper;
using Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Queries;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Entities.Membership;


namespace Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<AppRole, RoleDto>();
        }
    }
}
