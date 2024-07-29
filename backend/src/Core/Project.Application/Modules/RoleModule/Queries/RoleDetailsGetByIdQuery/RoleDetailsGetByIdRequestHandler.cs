using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.RoleModule.Queries.RoleDetailsGetByIdQuery
{
    class RoleDetailsGetByIdRequestHandler : IRequestHandler<RoleDetailsGetByIdRequest, RoleDetailsGetByIdResponse>
    {
        private readonly DbContext db;
        private readonly ILogger<RoleDetailsGetByIdRequestHandler> logger;

        public RoleDetailsGetByIdRequestHandler(DbContext db, ILogger<RoleDetailsGetByIdRequestHandler> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<RoleDetailsGetByIdResponse> Handle(RoleDetailsGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling RoleDetailsGetByIdRequest for Role ID: {RoleId}", request.Id);

            AppRole role = await db.Set<AppRole>().FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (role is null)
            {
                logger.LogWarning("Role with ID {RoleId} not found.", request.Id);
                throw new NotFoundException("Role not found");
            }

            logger.LogInformation("Role with ID {RoleId} found. Retrieving details.", request.Id);

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

            logger.LogInformation("Retrieved {MemberCount} members for Role ID {RoleId}.", dto.Members.Count(), request.Id);
            #endregion

            return dto;
        }
    }
}
