using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.RoleModule.Commands.RoleAddCommand;
using Project.Application.Modules.RoleModule.Commands.RoleEditCommand;
using Project.Application.Modules.RoleModule.Commands.RoleRemoveCommand;
using Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand.Queries.RoleGetAllQuery;

using Microsoft.AspNetCore.Identity;
using Project.Domain.Models.Entities.Membership;
using Project.Api.AppCode.Pipeline;
using Project.Application.Modules.RoleModule.Queries.RoleGetByIdQuery;
using Project.Application.Modules.RoleModule.Queries.RoleDetailsGetByIdQuery;
using Project.Application.Modules.RoleModule.Commands.ManageMemberCommand;
using Project.Application.Modules.RoleModule.Commands.ChangeAccessCommand;

namespace Project.Api.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public RolesController(IMediator mediator, RoleManager<AppRole> roleManager)
        {
            this.mediator = mediator;
           
        }


        [HttpGet("details/{id:int:min(1)}")]
        [Authorize("roles.details")]
        public async Task<IActionResult> GetDetailsById([FromRoute] RoleDetailsGetByIdRequest request)
        {
            request.Policies = AppClaimsTransformation.policies;

            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{id:int:min(1)}")]
        [Authorize("roles.getall")]
        public async Task<IActionResult> GetById([FromRoute] RoleGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [HttpGet]
        [Authorize("roles.getall")]
        public async Task<IActionResult> GetAll([FromRoute] RoleGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


  


        [HttpPost]
        [Authorize("roles.add")]
        public async Task<IActionResult> AddRole(RoleAddRequest request)
        {
            var res = await mediator.Send(request);
            return Ok(res);
        }

        [HttpPut("{id:int:min(1)}")]
        [Authorize("roles.edit")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] RoleEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("roles.delete")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] RoleRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }



        [HttpPost("manage-member/{memberId:int:min(1)}")]
        [Authorize("roles.manage-member")]
        public async Task<IActionResult> ManageMember([FromRoute] int memberId, [FromBody] ManageMemberRequest request)
        {
            request.MemberId = memberId;
            await mediator.Send(request);
            return Ok();
        } 
        
        [HttpPost("change-access/{roleId:int:min(1)}")]
        [Authorize("roles.change-access")]
        public async Task<IActionResult> ChangeAccess([FromRoute] int roleId, [FromBody] ChangeAccessRequest request)
        {
            request.RoleId = roleId;
            request.Policies=AppClaimsTransformation.policies;
            await mediator.Send(request);
            return Ok();
        }

    }
}
