using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.RoleModule.Commands.RoleAddCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.RolesModule.Commands
{
    public class RoleAddRequestHandler : IRequestHandler<RoleAddRequest, AppRole>
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly ILogger<RoleAddRequestHandler> logger;

        public RoleAddRequestHandler(RoleManager<AppRole> roleManager, ILogger<RoleAddRequestHandler> logger)
        {
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task<AppRole> Handle(RoleAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling RoleAddRequest for Name: {Name}", request.Name);

            var existingRole = await roleManager.FindByNameAsync(request.Name.ToUpperInvariant());
            if (existingRole != null)
            {
                logger.LogWarning("Role with Name: {Name} already exists.", request.Name);
                throw new EntityAlreadyExistsException(nameof(AppRole), request.Name);
            }

            var role = new AppRole
            {
                Name = request.Name,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                logger.LogInformation("Successfully created Role with Name: {Name}", request.Name);
                return role;
            }

            logger.LogError("Failed to create Role with Name: {Name}", request.Name);
            throw new EntityCreationFailedException(nameof(AppRole));
        }
    }
}
