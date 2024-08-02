using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.FacilitiesModule.Commands.FacilityAddCommand;
using Project.Application.Modules.FacilitiesModule.Commands.FacilityEditCommand;
using Project.Application.Modules.FacilitiesModule.Commands.FacilityRemoveCommand;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetAllQuery;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByIdQuery;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByPropertyIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public FacilitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] FacilityGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }
        [AllowAnonymous]
        [HttpGet("byproperty/{propertyid:int:min(1)}")]
        public async Task<IActionResult> GetByPropertyId([FromRoute] FacilityGetByPropertyIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] FacilityGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("facilities.add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] FacilityAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [Authorize("facilities.edit")]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] FacilityEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [Authorize("facilities.remove")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] FacilityRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
