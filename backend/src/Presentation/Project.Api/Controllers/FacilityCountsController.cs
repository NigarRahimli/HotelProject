using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountAddCommand;
using Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountEditCommand;
using Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountRemoveCommand;
using Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetAllQuery;
using Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityCountsController : ControllerBase
    {
        private readonly IMediator mediator;

        public FacilityCountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] FacilityCountGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] FacilityCountGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Add(FacilityCountAddRequest request) {
        
        var entity=await mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new {entity.Id},entity);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody]FacilityCountEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute]FacilityCountRemoveRequest request)
        {
           await mediator.Send(request);
            return NoContent();
        }

    }
}
