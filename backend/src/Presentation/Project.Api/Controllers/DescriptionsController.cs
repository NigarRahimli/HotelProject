using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.DescriptionsModule.Commands.DescriptionAddCommand;
using Project.Application.Modules.DescriptionsModule.Commands.DescriptionEditCommand;
using Project.Application.Modules.DescriptionsModule.Commands.DescriptionRemoveCommand;
using Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetAllQuery;
using Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescriptionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public DescriptionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] DescriptionGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] DescriptionGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("descriptions.add")]
        [HttpPost]
        public async Task<IActionResult> Add(DescriptionAddRequest request) {
        
        var entity=await mediator.Send(request);
        return CreatedAtAction(nameof(GetById), new {entity.Id},entity);
        }


        [Authorize("descriptions.edit")]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody]DescriptionEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("descriptions.delete")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute]DescriptionRemoveRequest request)
        {
           await mediator.Send(request);
            return NoContent();
        }

    }
}
