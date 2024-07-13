using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Project.Api.AppCode.Pipeline;
using Project.Application.Services;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Resume.Api.AppCode.Pipeline
{
    static class IdentityInjection
    {
        internal static IServiceCollection AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>()
             .AddRoles<AppRole>()
             .AddDefaultTokenProviders()
             .AddEntityFrameworkStores<DbContext>();


            services.AddScoped<SignInManager<AppUser>>();
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>>();


            
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IClaimsTransformation, AppClaimsTransformation>();

          

            services.Configure<IdentityOptions>(cfg =>
            {

                cfg.User.RequireUniqueEmail = true;
                //cfg.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";


                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                cfg.Lockout.AllowedForNewUsers = true;
                cfg.Lockout.MaxFailedAccessAttempts = 3;

                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 1;
                cfg.Password.RequiredLength = 3;
            });

            services.Configure<JwtConfigurationOption>(cfg => configuration.Bind("jwt", cfg));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["jwt:issuer"],
                        ValidAudience = configuration["jwt:audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]!)),
                        LifetimeValidator = ValidateExpiration
                    };

                    options.Events = new JwtBearerEvents
                    {

                        OnChallenge = Challenge

                    };
                });

            services.AddAuthorization(cfg => {

                foreach (var item in AppClaimsTransformation.policies)
                {
                    cfg.AddPolicy(item, p =>
                    {
                        //p.RequireClaim(item, "1");

                        p.RequireAssertion(handler => handler.User.IsInRole("SUPERADMIN") || handler.User.HasClaim(item, "1"));
                        //p.RequireAssertion(handler => handler.User.HasClaim(item, "1"));
                    });
                }

            });



            return services;
        }

        private static Task Challenge(JwtBearerChallengeContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues accessTokenPairs))
                return Task.CompletedTask;

            string accessToken = accessTokenPairs.FirstOrDefault()?.Replace("Bearer ", string.Empty);

            if (string.IsNullOrWhiteSpace(accessToken))
                return Task.CompletedTask;

            var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            if (token.ValidTo < DateTime.UtcNow)
                throw new SecurityTokenExpiredException();

            return Task.CompletedTask;
        }

        private static bool ValidateExpiration(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return DateTime.UtcNow < expires;
        }
    }
}
