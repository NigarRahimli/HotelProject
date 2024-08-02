using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Models.Entities.Membership;

namespace Project.DataAccessLayer.Contexts
{
    public static class DataContextSeed
    {
        public static IApplicationBuilder SeedUser(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var superAdminRole = "SuperAdmin";

                var role = roleManager.FindByNameAsync(superAdminRole).Result;


                if (role is null)
                {
                    role = new AppRole
                    {
                        Name = superAdminRole,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    roleManager.CreateAsync(role).Wait();
                }

                string superAdminEmail = configuration["SuperAdmin:Email"]!;
                string superAdminPassword = configuration["SuperAdmin:Password"]!;

                var user = userManager.FindByEmailAsync(superAdminEmail).Result;

                if (user is null)
                {
                    user = new AppUser
                    {
                        UserName = superAdminEmail,
                        Email = superAdminEmail,
                        EmailConfirmed = true,
                        ProfileImgUrl= "/uploads/default/profile_avatar.png"
                        
                    };

                    var result = userManager.CreateAsync(user, superAdminPassword).Result;

                    if (!result.Succeeded)
                        throw new Exception("User cant be created!");
                }


                //if (!userManager.IsInRoleAsync(user, superAdminRole.ToUpper()).Result)
                //{
                //    if (!userManager.AddToRoleAsync(user, superAdminRole).Result.Succeeded)
                //        throw new Exception($"User cant be added to role: {superAdminRole}!");
                //}


                var userRole = db.Set<AppUserRole>().FirstOrDefault(m => m.UserId == user.Id && m.RoleId == role.Id);

                if (userRole is null)
                {
                    db.Set<AppUserRole>().Add(new AppUserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });

                    db.SaveChanges();
                }


            }


            return app;
        }
    }
}
