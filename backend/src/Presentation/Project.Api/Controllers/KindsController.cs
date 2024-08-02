using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.KindsModule.Commands.KindAddCommand;
using Project.Application.Modules.KindsModule.Commands.KindEditCommand;
using Project.Application.Modules.KindsModule.Commands.KindRemoveCommand;
using Project.Application.Modules.KindsModule.Queries.KindGetAllQuery;
using Project.Application.Modules.KindsModule.Queries.KindGetByIdQuery;

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

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] KindGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] KindGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [Authorize("kinds.add")]
        [HttpPost]
        public async Task<IActionResult> Add(KindAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [Authorize("kinds.edit")]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] KindEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("kinds.remove")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] KindRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }



    }
}
