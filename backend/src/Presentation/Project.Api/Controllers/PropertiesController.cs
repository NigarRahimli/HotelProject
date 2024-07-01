using MediatR;
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

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearby([FromBody] PropertyGetAllNearbyRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("latest/{take:int:min(1)}")]
        public async Task<IActionResult> GetLatest([FromRoute] PropertyGetAllLatestRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }


        [HttpGet("rated/{take:int:min(1)}")]
        public async Task<IActionResult> GetTopRated([FromRoute] PropertyGetAllTopRatedRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }


        [HttpGet("featured/{take:int:min(1)}")]
        public async Task<IActionResult> GetFeatured([FromRoute] PropertyGetAllFeaturedRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{page:int:min(1)}/size/{size:int:min(2)}")]
        public async Task<IActionResult> GetPaged([FromRoute] PropertyPagedRequest request)
        {
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
