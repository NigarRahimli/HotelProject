using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize("propertyimages.getall")]
        [HttpGet("{propertyid:int:min(1)}")]
        public async Task<IActionResult> GetByPropertyId([FromRoute] PropertyImagesGetByPropertyIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("propertyimages.getall")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] PropertyImageGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("propertyimages.add")]
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

        [Authorize("propertyimages.edit")]

        [HttpPut("{propertyId:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int propertyId, [FromForm] PropertyImagesEditRequest request)
        {
            request.PropertyId = propertyId;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


      

    }
}
