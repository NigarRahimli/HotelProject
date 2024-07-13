using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities.Membership;


namespace Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Queries.RoleGetAllQuery
{
    class RoleGetAllRequestHandler : IRequestHandler<RoleGetAllRequest, IEnumerable<RoleDto>>
    {

        private readonly IMapper mapper;
        private readonly RoleManager<AppRole> roleManager;

        public RoleGetAllRequestHandler(IMapper mapper, RoleManager<AppRole> roleManager)
        {

            this.mapper = mapper;
            this.roleManager = roleManager;
        }
        public async Task<IEnumerable<RoleDto>> Handle(RoleGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await roleManager.Roles.ToListAsync(cancellationToken);
            var roleDtos = mapper.Map<IEnumerable<RoleDto>>(entities);
 
            return roleDtos;
        }
    }
}
