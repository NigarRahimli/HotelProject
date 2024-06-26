using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Extensions;


namespace Project.DataAccessLayer.Contexts
{
    class DataContext : IdentityDbContext<AppUser, AppRole, int, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {

        private readonly IHttpContextAccessor httpContextAccessor;

        public DataContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changes = this.ChangeTracker.Entries<IAuditableEntity>();

            if (changes != null)
            {
                foreach (var entry in changes.Where(m => m.State == EntityState.Added
                || m.State == EntityState.Modified
                || m.State == EntityState.Deleted))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedBy = httpContextAccessor.HttpContext.GetUserIdExtension();
                            entry.Entity.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            entry.Property(m => m.CreatedBy).IsModified = false;
                            entry.Property(m => m.CreatedAt).IsModified = false;
                            entry.Entity.LastModifiedBy = httpContextAccessor.HttpContext.GetUserIdExtension();
                            entry.Entity.LastModifiedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.Property(m => m.CreatedBy).IsModified = false;
                            entry.Property(m => m.CreatedAt).IsModified = false;
                            entry.Property(m => m.LastModifiedBy).IsModified = false;
                            entry.Property(m => m.LastModifiedAt).IsModified = false;
                            entry.Entity.DeletedBy = httpContextAccessor.HttpContext.GetUserIdExtension();
                            entry.Entity.DeletedAt = DateTime.UtcNow;
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }

}
