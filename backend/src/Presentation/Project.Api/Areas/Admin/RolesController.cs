using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.RoleModule.Commands.RoleAddCommand;

namespace Project.Api.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator mediator;

        public RolesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize("roles.add")]
        public async Task<IActionResult> AddRole(RoleAddRequest request)
        {
            var res = await mediator.Send(request);
            return Ok(res);
        }

        //[HttpPost("assign-policy")]
        //[Authorize("roles.assignPolicy")]
        //public async Task<IActionResult> AssignPolicy(PolicyAssignCommand request)
        //{
        //    await mediator.Send(request);
        //    return Ok();
        //}

        //[HttpPost("assign-user-role")]
        //[Authorize("users.assignRole")]
        //public async Task<IActionResult> AssignUserRole(UserAssignRoleCommand request)
        //{
        //    await mediator.Send(request);
        //    return Ok();
        //}
    }
}
