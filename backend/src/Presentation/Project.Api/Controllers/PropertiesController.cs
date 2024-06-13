using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.PropertiesModule.Commands.PropertyAddCommand;
using Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand;
using Project.Application.Modules.PropertiesModule.Commands.PropertyRemoveCommand;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllQuery;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PropertiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] PropertyGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] PropertyGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Add(PropertyAddRequest request) {
        
        var entity=await mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new {entity.Id},entity);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody]PropertyEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute]PropertyRemoveRequest request)
        {
           await mediator.Send(request);
            return NoContent();
        }

    }
}
