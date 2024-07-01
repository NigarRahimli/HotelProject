using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.LocationsModule.Commands.LocationAddCommand;
using Project.Application.Modules.LocationsModule.Commands.LocationEditCommand;
using Project.Application.Modules.LocationsModule.Commands.LocationRemoveCommand;
using Project.Application.Modules.LocationsModule.Queries.KindGetAllQuery;
using Project.Application.Modules.LocationsModule.Queries.LocationGetAllQuery;
using Project.Application.Modules.LocationsModule.Queries.LocationGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LocationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] LocationGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [HttpGet("byuser")]
        public async Task<IActionResult> GetByUserId([FromRoute] LocationGetByUserIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] LocationGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Add(LocationAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] LocationEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] LocationRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
