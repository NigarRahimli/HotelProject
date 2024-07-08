using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.KindsModule.Commands.KindAddCommand;
using Project.Application.Modules.KindsModule.Commands.KindEditCommand;
using Project.Application.Modules.KindsModule.Commands.KindRemoveCommand;
using Project.Application.Modules.KindsModule.Queries.KindGetAllQuery;
using Project.Application.Modules.KindsModule.Queries.KindGetByIdQuery;



namespace Project.Api.Areas.Admin
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

        [HttpGet("{id:int:min(1)}")]
        [Authorize("kinds.getall")]
        public async Task<IActionResult> GetById([FromRoute] KindGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromRoute] KindGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost]
        [Authorize("kinds.add")]
        public async Task<IActionResult> Add(KindAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [HttpPut("{id:int:min(1)}")]
        [Authorize("kinds.edit")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] KindEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("kinds.delete")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] KindRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
