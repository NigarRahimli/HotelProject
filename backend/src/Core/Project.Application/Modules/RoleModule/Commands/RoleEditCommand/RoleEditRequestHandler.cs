using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.RoleModule.Commands.RoleEditCommand
{
    class RoleEditRequestHandler : IRequestHandler<RoleEditRequest, AppRole>
    {
        private readonly RoleManager<AppRole> roleManager;

        public RoleEditRequestHandler(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<AppRole> Handle(RoleEditRequest request, CancellationToken cancellationToken)
        {

            var entity = await roleManager.FindByIdAsync(request.Id.ToString());

            entity.Name = request.Name;
            await roleManager.UpdateAsync(entity);

            return entity;
        }
    }
}
