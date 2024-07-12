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
using Project.Application.Modules.AccountModule.Commands.EmailConfirmationCommand;
using Project.Application.Modules.AccountModule.Commands.UploadProfilePhotoCommand;
using Project.Application.Modules.AccountModule.Commands.EditProfilePhotoCommand;
using Project.Application.Modules.AccountModule.Commands.RemoveProfilePhotoCommand;
using Project.Application.Modules.AccountModule.Commands.SendPhoneConfirmationCommand;
using Project.Application.Modules.AccountModule.Commands.ConfirmPhoneCommand;
using Microsoft.AspNetCore.Identity.Data;
using Project.Application.Modules.AccountModule.Commands.SendForgotPasswordEmailCommand;
using Project.Application.Modules.AccountModule.Commands.ResetPasswordCommand;
using System.Web;
using Project.Application.Modules.AccountModule.Commands.ChangePasswordCommand;

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

         
            var firstUser = await db.Set<AppUser>().OrderBy(u => u.Id).FirstOrDefaultAsync();

            if (firstUser != null && firstUser.Email == request.Email)
            {
                var claimsIdentity = (ClaimsIdentity)principal.Identity;
                claimsIdentity.AddClaim(new Claim("FirstUserClaim", "true"));
            }
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
                UserId = userId,
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

        [HttpPut("uploadImg")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImg([FromForm]UploadProfilePhotoRequest request)
        {
            await mediator.Send(request);
            return Ok(new { message = "Profile image uploaded" });
        }

        [HttpPut("changeImg")]
        [AllowAnonymous]
        public async Task<IActionResult> EditImg([FromForm] EditProfilePhotoRequest request)
        {
            await mediator.Send(request);
            return Ok(new { message = "Profile image changed" });
        }

        [HttpDelete("removeImg")]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveImg([FromRoute]RemoveProfilePhotoRequest request)
        {
            await mediator.Send(request);
            return Ok(new { message = "Profile image removed" });
        }


        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]EmailConfirmationRequest request)
        {
            await mediator.Send(request);
            return Ok(new { message = "Email confirmation successful." });
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> RequestPhoneConfirmation(SendPhoneConfirmationRequest request)
        {
    
                await mediator.Send(request);
                return Ok("Confirmation code sent successfully.");
          
        }
        [HttpPost("confirm-phone")]
        public async Task<IActionResult> ConfirmPhone(ConfirmCodeRequest request)
        {
           
                await mediator.Send(request);
                return Ok("Phone number confirmed successfully.");
        
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] SendForgotPasswordEmailRequest request)
        {
            await mediator.Send(request);
            return Ok("Forgot pasword email was sent successfully.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetCurrentPasswordRequest request)
        {
            await mediator.Send(request);
            return Ok("New password was set");
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {     
                await mediator.Send(request);
                return Ok(new { message = "Password changed successfully." });
        
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
