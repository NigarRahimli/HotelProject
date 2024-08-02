using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryAddCommand;
using Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryEditCommand;
using Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryRemoveCommand;
using Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetAllQuery;
using Project.Application.Modules.ReviewCategoriesModule.Queries.ReviewCategoryGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewCategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReviewCategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize("reviewcategories.getall")]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] ReviewCategoryGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("reviewcategories.getall")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] ReviewCategoryGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("reviewcategories.add")]
        [HttpPost]
        public async Task<IActionResult> Add(ReviewCategoryAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [Authorize("reviewcategories.edit")]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ReviewCategoryEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("reviewcategories.remove")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] ReviewCategoryRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
