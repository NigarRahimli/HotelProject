using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.AppCode.Pipeline;
using Project.Application.Modules.RoleModule.Commands.AssignPoliciesToRoleCommand;
using System.Reflection;

namespace Project.Api.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PoliciesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet()]
        public IActionResult GetAllPolicies()
        {
            var policies = AppClaimsTransformation.policies;

            return Ok(policies);
        }


       
    }
   

}
