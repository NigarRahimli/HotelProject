using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.SafetiesModule.Commands.SafetyAddCommand;
using Project.Application.Modules.SafetiesModule.Commands.SafetyEditCommand;
using Project.Application.Modules.SafetiesModule.Commands.SafetyRemoveCommand;
using Project.Application.Modules.SafetiesModule.Queries.SafetyGetAllQuery;
using Project.Application.Modules.SafetiesModule.Queries.SafetyGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafetiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public SafetiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] SafetyGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] SafetyGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromForm]SafetyAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] SafetyEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] SafetyRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
