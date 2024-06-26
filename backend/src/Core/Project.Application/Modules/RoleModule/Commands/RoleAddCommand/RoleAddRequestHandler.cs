using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Modules.RoleModule.Commands.RoleAddCommand;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.RolesModule.Commands
{
    public class RoleAddRequestHandler : IRequestHandler<RoleAddRequest, AppRole>
    {
        private readonly RoleManager<AppRole> roleManager;

        public RoleAddRequestHandler(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<AppRole> Handle(RoleAddRequest request, CancellationToken cancellationToken)
        {
           
            var existingRole = await roleManager.FindByNameAsync(request.RoleName.ToUpperInvariant());
            if (existingRole != null)
            {
                throw new EntityAlreadyExistsException(nameof(AppRole), request.RoleName);
            }


            var role = new AppRole
            {
                Name = request.RoleName,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return role;
            }

            throw new EntityCreationFailedException(nameof(AppRole));
        }
    }
}
