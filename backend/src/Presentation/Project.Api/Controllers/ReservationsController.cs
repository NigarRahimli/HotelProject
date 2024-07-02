using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.ReservationModule.Commands.ReservationAddCommand;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetAllQuery;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetByIdQuery;
using Project.Application.Modules.ReservationsModule.Commands.ReservationEditCommand;
using Project.Application.Modules.ReservationsModule.Commands.ReservationRemoveCommand;


namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReservationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] ReservationGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] ReservationGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ReservationAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ReservationEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] ReservationRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
