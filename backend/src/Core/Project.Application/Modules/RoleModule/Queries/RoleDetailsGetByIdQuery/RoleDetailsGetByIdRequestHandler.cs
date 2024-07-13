
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.RoleModule.Queries.RoleDetailsGetByIdQuery
{
    class RoleDetailsGetByIdRequestHandler : IRequestHandler<RoleDetailsGetByIdRequest, RoleDetailsGetByIdResponse>
    {
        private readonly DbContext db;

        public RoleDetailsGetByIdRequestHandler(DbContext db)
        {
            this.db = db;
        }

        public async Task<RoleDetailsGetByIdResponse> Handle(RoleDetailsGetByIdRequest request, CancellationToken cancellationToken)
        {
            AppRole role = await db.Set<AppRole>().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (role is null)
                throw new NotFoundException();

            var dto = new RoleDetailsGetByIdResponse
            {
                Id = role.Id,
                Name = role.Name,
            };

            #region Policies
            var rolePolicies = db.Set<AppRoleClaim>().Where(m => m.RoleId == request.Id && m.ClaimValue == "1").Select(m => m.ClaimType);


            dto.Policies = (from p in request.Policies
                            join rp in rolePolicies on p equals rp into leftSet
                            from ls in leftSet.DefaultIfEmpty()
                            select new PolicyDto
                            {
                                Name = p,
                                IsSelected = ls != null
                            });
            #endregion

            #region Members
            dto.Members = await (from u in db.Set<AppUser>()
                                 join ur in db.Set<AppUserRole>().Where(m => m.RoleId == role.Id) on u.Id equals ur.UserId into leftSet
                                 from ls in leftSet.DefaultIfEmpty()
                                 select new RoleMemberDto
                                 {
                                     Id = u.Id,
                                     Name = $"{u.UserName} ({u.Email})",
                                     IsSelected = ls != null
                                 }).ToListAsync();
            #endregion

            return dto;
        }
    }
}
