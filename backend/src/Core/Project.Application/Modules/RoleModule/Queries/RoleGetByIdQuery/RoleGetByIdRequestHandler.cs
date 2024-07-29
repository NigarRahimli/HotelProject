using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Queries;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.RoleModule.Queries.RoleGetByIdQuery
{
    class RoleGetByIdRequestHandler : IRequestHandler<RoleGetByIdRequest, RoleDto>
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly IMapper mapper;
        private readonly ILogger<RoleGetByIdRequestHandler> logger;

        public RoleGetByIdRequestHandler(RoleManager<AppRole> roleManager, IMapper mapper, ILogger<RoleGetByIdRequestHandler> logger)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<RoleDto> Handle(RoleGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling RoleGetByIdRequest for Role ID: {RoleId}", request.Id);

            var entity = await roleManager.FindByIdAsync(request.Id.ToString());

            if (entity == null)
            {
                logger.LogWarning("Role with ID {RoleId} not found.", request.Id);
                throw new NotFoundException($"Role not found.");
            }

            var roleDto = mapper.Map<RoleDto>(entity);

            logger.LogInformation("Successfully retrieved Role with ID: {RoleId}", request.Id);

            return roleDto;
        }
    }
}
