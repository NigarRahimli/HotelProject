using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand;
using Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand;
using Project.Application.Modules.AmenitiesModule.Commands.AmenityRemoveCommand;
using Project.Application.Modules.AmenitiesModule.Queries.AmenityGetAllQuery;
using Project.Application.Modules.AmenitiesModule.Queries.AmenityGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public AmenitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] AmenityGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] AmenityGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("amenities.add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]AmenityAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }


        [Authorize("amenities.edit")]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] AmenityEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("amenities.remove")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] AmenityRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
