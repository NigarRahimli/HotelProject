using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.RoleModule.Commands.RoleEditCommand
{
    public class RoleEditRequestHandler : IRequestHandler<RoleEditRequest, AppRole>
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly ILogger<RoleEditRequestHandler> logger;

        public RoleEditRequestHandler(RoleManager<AppRole> roleManager, ILogger<RoleEditRequestHandler> logger)
        {
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task<AppRole> Handle(RoleEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling role edit request for Role ID: {RoleId}", request.Id);

            var entity = await roleManager.FindByIdAsync(request.Id.ToString());
            if (entity == null)
            {
                logger.LogWarning("Role with ID {RoleId} not found", request.Id);
                throw new NotFoundException($"Role not found.");
            }

            entity.Name = request.Name;
            var result = await roleManager.UpdateAsync(entity);

            if (result.Succeeded)
            {
                logger.LogInformation("Successfully updated Role ID: {RoleId} with new name: {RoleName}", request.Id, request.Name);
            }
            else
            {
                logger.LogError("Failed to update Role ID: {RoleId}. Errors: {Errors}", request.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return entity;
        }
    }
}
