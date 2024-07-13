using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Repositories;
using Project.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.RoleModule.Commands.RoleRemoveCommand
{
    class RoleRemoveRequestHandler : IRequestHandler<RoleRemoveRequest>
    {
        private readonly RoleManager<AppRole> roleManager;

        public RoleRemoveRequestHandler(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task Handle(RoleRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity = await roleManager.FindByIdAsync(request.Id.ToString());
            await roleManager.DeleteAsync(entity);
           
        }
    }
}
