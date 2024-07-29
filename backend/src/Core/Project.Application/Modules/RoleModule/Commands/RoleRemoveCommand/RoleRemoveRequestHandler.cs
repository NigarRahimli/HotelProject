using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.RoleModule.Commands.RoleRemoveCommand
{
    public class RoleRemoveRequestHandler : IRequestHandler<RoleRemoveRequest>
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly ILogger<RoleRemoveRequestHandler> logger;

        public RoleRemoveRequestHandler(RoleManager<AppRole> roleManager, ILogger<RoleRemoveRequestHandler> logger)
        {
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task Handle(RoleRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling role remove request for Role ID: {RoleId}", request.Id);

            var entity = await roleManager.FindByIdAsync(request.Id.ToString());
            if (entity == null)
            {
                logger.LogWarning("Role with ID {RoleId} not found", request.Id);
                throw new NotFoundException($"Role with ID {request.Id} not found.");
            }

            try
            {
                var result = await roleManager.DeleteAsync(entity);

                if (result.Succeeded)
                {
                    logger.LogInformation("Successfully deleted Role ID: {RoleId}", request.Id);
                }
                else
                {
                    logger.LogError("Failed to delete Role ID: {RoleId}. Errors: {Errors}", request.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new OperationFailedException($"Failed to delete Role");
                }
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "An error occurred while deleting Role ID: {RoleId}", request.Id);
                throw new OperationFailedException($"Failed to delete Role due to database constraints.");
            }

        }
    }
}
