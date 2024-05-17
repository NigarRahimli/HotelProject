using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.KindsModule.Commands.KindAddCommand;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KindsController : ControllerBase
    {
        private readonly IMediator mediator;

        public KindsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(KindAddRequest request) {
        
        var entity=await mediator.Send(request);
        return Ok(entity); 
        }

    }
}
