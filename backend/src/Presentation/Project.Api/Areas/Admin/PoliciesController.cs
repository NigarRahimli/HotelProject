using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.AppCode.Pipeline;
using System.Reflection;

namespace Project.Api.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetAllPolicies()
        {
            var policies = AppClaimsTransformation.policies;

            return Ok(policies);
        }
    }


}
