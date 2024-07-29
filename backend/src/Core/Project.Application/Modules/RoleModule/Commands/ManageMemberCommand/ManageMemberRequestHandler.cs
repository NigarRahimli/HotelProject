using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.RoleModule.Commands.ManageMemberCommand
{
    public class ManageMemberRequestHandler : IRequestHandler<ManageMemberRequest>
    {
        private readonly DbContext db;
        private readonly IActionContextAccessor ctx;
        private readonly ILogger<ManageMemberRequestHandler> logger;

        public ManageMemberRequestHandler(DbContext db, IActionContextAccessor ctx, ILogger<ManageMemberRequestHandler> logger)
        {
            this.db = db;
            this.ctx = ctx;
            this.logger = logger;
        }

        public async Task Handle(ManageMemberRequest request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(ctx.ActionContext.HttpContext.User.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier))?.Value);

            logger.LogInformation("Handling ManageMemberRequest for MemberId: {MemberId} and RoleId: {RoleId}", request.MemberId, request.RoleId);

           
            if (request.MemberId == userId)
            {
                logger.LogWarning("User with Id: {MemberId} attempted to change their own roles.", userId);
                throw new BadRequestException("User cannot change their own roles.");
            }

            // Check if the role exists
            var roleExists = await db.Set<AppRole>().AnyAsync(r => r.Id == request.RoleId, cancellationToken);
            if (!roleExists)
            {
                logger.LogWarning("Role with RoleId: {RoleId} does not exist.", request.RoleId);
                throw new NotFoundException("Role does not exist.");
            }

            // Check if the user exists
            var userExists = await db.Set<AppUser>().AnyAsync(u => u.Id == request.MemberId, cancellationToken);
            if (!userExists)
            {
                logger.LogWarning("User with Id: {MemberId} does not exist.", request.MemberId);
                throw new NotFoundException("User does not exist.");
            }

            var table = db.Set<AppUserRole>();
            AppUserRole userRole = default;

            if (request.IsSelected)
            {
                userRole = await table.FirstOrDefaultAsync(m => m.UserId == request.MemberId && m.RoleId == request.RoleId, cancellationToken);

                if (userRole is not null)
                {
                    logger.LogWarning("User with Id: {MemberId} is already assigned to RoleId: {RoleId}.", request.MemberId, request.RoleId);
                    throw new BadRequestException("User is already assigned to this role.");
                }

                userRole = new AppUserRole
                {
                    UserId = request.MemberId,
                    RoleId = request.RoleId
                };

                await table.AddAsync(userRole, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                logger.LogInformation("Assigned RoleId: {RoleId} to MemberId: {MemberId}.", request.RoleId, request.MemberId);
            }
            else
            {
                userRole = await table.FirstOrDefaultAsync(m => m.UserId == request.MemberId && m.RoleId == request.RoleId, cancellationToken);

                if (userRole is null)
                {
                    logger.LogWarning("User with MemberId: {MemberId} is not assigned to RoleId: {RoleId}.", request.MemberId, request.RoleId);
                    throw new BadRequestException("User is not assigned to this role.");
                }

                table.Remove(userRole);
                await db.SaveChangesAsync(cancellationToken);
                logger.LogInformation("Removed RoleId: {RoleId} from MemberId: {MemberId}.", request.RoleId, request.MemberId);
            }

            logger.LogInformation("Successfully handled ManageMemberRequest for MemberId: {MemberId} and RoleId: {RoleId}", request.MemberId, request.RoleId);
        }
    }
}
