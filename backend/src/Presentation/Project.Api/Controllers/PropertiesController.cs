using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.PropertiesModule.Commands.PropertyAddCommand;
using Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand;
using Project.Application.Modules.PropertiesModule.Commands.PropertyRemoveCommand;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllFeatured;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllLatestQuery;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllQuery;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllTopRated;
using Project.Application.Modules.PropertiesModule.Queries.PropertyGetByIdQuery;
using Project.Application.Modules.PropertiesModule.Queries.PropertyPagedQuery;

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

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] PropertyGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] PropertyGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [AllowAnonymous]
        [HttpPost("nearby/{take:int:min(1)}")]
        public async Task<IActionResult> GetNearby(int take,[FromBody] PropertyGetAllNearbyRequest request)
        {
            request.Take = take;
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("latest/{take:int:min(1)}")]
        public async Task<IActionResult> GetLatest([FromRoute] PropertyGetAllLatestRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("rated/{take:int:min(1)}")]
        public async Task<IActionResult> GetTopRated([FromRoute] PropertyGetAllTopRatedRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("featured/{take:int:min(1)}")]
        public async Task<IActionResult> GetFeatured([FromRoute] PropertyGetAllFeaturedRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("{page:int:min(1)}/size/{size:int:min(2)}")]
        public async Task<IActionResult> GetPaged(int page, int size, [FromBody] PropertyPagedRequest request)
        {
            request.Page = page;
            request.Size = size;

            var response = await mediator.Send(request);

            return Ok(response);
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
