using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageAddCommand;
using Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageEditCommand;
using Project.Application.Modules.PropertyImagesModule.Queries.PropertyImageGetAllQuery;
using Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery;
using Project.Domain.Models.Entities;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PropertyImagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{propertyid:int:min(1)}")]
        public async Task<IActionResult> GetByPropertyId([FromRoute] PropertyImagesGetByPropertyIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] PropertyImageGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost("{propertyId:int:min(1)}")]
        public async Task<IActionResult> Add([FromRoute]int propertyId,[FromForm] PropertyImagesAddRequest request)
        {
            request.PropertyId = propertyId;
            var entity = await mediator.Send(request);
          
            var createdEntities = entity as List<PropertyImage> ?? entity.ToList();
            if (createdEntities.Count == 0)
            {
                return BadRequest("No images were added.");
            }

            return CreatedAtAction(nameof(GetByPropertyId), new { propertyId = request.PropertyId }, createdEntities);
        }

        [HttpPut("{propertyId:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int propertyId, [FromForm] PropertyImageEditRequest request)
        {
            request.PropertyId = propertyId;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        //[HttpDelete("{id:int:min(1)}")]
        //public async Task<IActionResult> Remove([FromRoute] PropertyImageRemoveRequest request)
        //{
        //    await mediator.Send(request);
        //    return NoContent();
        //}

    }
}
