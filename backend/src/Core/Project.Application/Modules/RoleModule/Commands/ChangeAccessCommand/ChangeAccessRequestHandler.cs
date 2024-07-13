
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.RoleModule.Commands.ChangeAccessCommand
{
    class ChangeAccessRequestHandler : IRequestHandler<ChangeAccessRequest>
    {
        private readonly DbContext db;

        public ChangeAccessRequestHandler(DbContext db)
        {
            this.db = db;
        }
        public async Task Handle(ChangeAccessRequest request, CancellationToken cancellationToken)
        {
            if (!request.Policies.Contains(request.PolicyName))
            {
                throw new NotFoundException("Policy does not exist.");
            }
            var table = db.Set<AppRoleClaim>();
            AppRoleClaim claim = default;

            if (request.IsSelected)
            {
                claim = await table.FirstOrDefaultAsync(m => m.RoleId == request.RoleId && m.ClaimType == request.PolicyName);

                if (claim is not null && claim.ClaimValue == "1")
                {
                    throw new BadRequestException("artiq icaze verilib");
                }
                else if (claim is not null)
                {
                    claim.ClaimValue = "1";
                    await db.SaveChangesAsync(cancellationToken);
                }
                else
                {
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
                claim = await table.FirstOrDefaultAsync(m => m.RoleId == request.RoleId && m.ClaimType == request.PolicyName);

                if (claim is null)
                {
                    throw new BadRequestException("icaze movcud deyil");
                }
                else
                {
                    table.Remove(claim);
                    await db.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
