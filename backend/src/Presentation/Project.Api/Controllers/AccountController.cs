using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities.Membership;
using Project.Infrastructure.Abstracts;
using System.Security.Claims;
using Project.Application.Modules.AccountModule.Commands.SignupCommand;
using Resume.Application.Modules.AccountModule.Commands.SigninCommand;
using Resume.Application.Modules.AccountModule.Commands.TokenRefreshCommand;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IConfiguration configuration;
        private readonly IJwtService jwtService;
        private readonly DbContext db;

        public AccountController(IMediator mediator, IConfiguration configuration, IJwtService jwtService, DbContext db)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.jwtService = jwtService;
            this.db = db;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Signin(SigninRequest request)
        {
            var principal = await mediator.Send(request);

            int userId = Convert.ToInt32(principal.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier)).Value);

            string token = jwtService.GenerateAccessToken(principal);
            string refreshToken = jwtService.GenerateRefreshToken(token);

            var table = db.Set<AppUserToken>();

            var lastTokenRecord = await table.FirstOrDefaultAsync(m => m.UserId == userId && m.LoginProvider.Equals("REFRESH_TOKEN") && m.IsActive == true);
            if (lastTokenRecord is not null)
            {
                lastTokenRecord.IsActive = false;
                await db.SaveChangesAsync();
            }

            var tokenRecord = new AppUserToken
            {
                UserId = Convert.ToInt32(principal.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier)).Value),
                LoginProvider = "REFRESH_TOKEN",
                Name = refreshToken,
                Value = "REFRESH_TOKEN",
                IsActive = true,
                Expired = DateTime.UtcNow.AddDays(1),
            };

            table.Add(tokenRecord);
            await db.SaveChangesAsync();

            return Ok(new
            {
                accessToken = token,
                refreshToken = refreshToken
            });
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(SignupRequest request)
        {
            await mediator.Send(request);
            return Ok(new { message = "Signup successful. Please check your email to confirm your account." });
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromRoute] TokenRefreshRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
