using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Queries;
using Project.Domain.Models.Entities.Membership;

namespace Project.Application.Modules.RoleModule.Queries.RoleGetByIdQuery
{
    class RoleGetByIdRequestHandler : IRequestHandler<RoleGetByIdRequest, RoleDto>
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly IMapper mapper;

        public RoleGetByIdRequestHandler(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        public async Task<RoleDto> Handle(RoleGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await roleManager.FindByIdAsync(request.Id.ToString());
            var roleDto = mapper.Map<RoleDto>(entity);
            return roleDto;
        }
    }
}
