using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.RoleModule.Commands.ChangeAccessCommand
{
    public class ChangeAccessRequestHandler : IRequestHandler<ChangeAccessRequest>
    {
        private readonly DbContext db;
        private readonly ILogger<ChangeAccessRequestHandler> logger;

        public ChangeAccessRequestHandler(DbContext db, ILogger<ChangeAccessRequestHandler> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task Handle(ChangeAccessRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling ChangeAccessRequest for RoleId: {RoleId} and PolicyName: {PolicyName}", request.RoleId, request.PolicyName);

            var roleExists = await db.Set<AppRole>().AnyAsync(r => r.Id == request.RoleId, cancellationToken);
            if (!roleExists)
            {
                logger.LogWarning("Role with RoleId: {RoleId} does not exist.", request.RoleId);
                throw new NotFoundException("Role does not exist.");
            }

            if (!request.Policies.Contains(request.PolicyName))
            {
                logger.LogWarning("Policy with name {PolicyName} does not exist.", request.PolicyName);
                throw new NotFoundException("Policy does not exist.");
            }

            var table = db.Set<AppRoleClaim>();
            AppRoleClaim claim = default;

            if (request.IsSelected)
            {
                claim = await table.FirstOrDefaultAsync(m => m.RoleId == request.RoleId && m.ClaimType == request.PolicyName, cancellationToken);

                if (claim is not null && claim.ClaimValue == "1")
                {
                    logger.LogWarning("Policy {PolicyName} is already assigned to RoleId: {RoleId}.", request.PolicyName, request.RoleId);
                    throw new BadRequestException("Policy already assigned.");
                }
                else if (claim is not null)
                {
                    logger.LogInformation("Updating existing claim for PolicyName: {PolicyName} and RoleId: {RoleId}.", request.PolicyName, request.RoleId);
                    claim.ClaimValue = "1";
                    await db.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    logger.LogInformation("Adding new claim for PolicyName: {PolicyName} and RoleId: {RoleId}.", request.PolicyName, request.RoleId);
                    claim = new AppRoleClaim
                    {
                        RoleId = request.RoleId,
                        ClaimType = request.PolicyName,
                        ClaimValue = "1"
                    };
                    await table.AddAsync(claim, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                claim = await table.FirstOrDefaultAsync(m => m.RoleId == request.RoleId && m.ClaimType == request.PolicyName, cancellationToken);

                if (claim is null)
                {
                    logger.LogWarning("Claim for PolicyName: {PolicyName} and RoleId: {RoleId} does not exist.", request.PolicyName, request.RoleId);
                    throw new BadRequestException("Policy not assigned.");
                }
                else
                {
                    logger.LogInformation("Removing claim for PolicyName: {PolicyName} and RoleId: {RoleId}.", request.PolicyName, request.RoleId);
                    table.Remove(claim);
                    await db.SaveChangesAsync(cancellationToken);
                }
            }

            logger.LogInformation("Successfully handled ChangeAccessRequest for RoleId: {RoleId} and PolicyName: {PolicyName}", request.RoleId, request.PolicyName);
        }
    }
}
