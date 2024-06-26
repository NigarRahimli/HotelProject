using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.Module.Commands.EditCommand;
using Project.Application.Modules.ReviewModule.Commands.ReviewRemoveCommand;
using Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand;
using Project.Application.Modules.ReviewsModule.Queries.GetAveragePerCategoryQuery;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAveragePerCategoryQuery;
using Project.Application.Modules.ReviewsModule.Queries.ReviewGetAverageQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReviewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("average-stars-per-category/{propertyid:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<ReviewAverageDto>>> GetAverageReviewPerCategory([FromRoute] ReviewGetAveragePerCategoryRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("total-average-reviews/{propertyid:int:min(1)}")]
        public async Task<ActionResult<double>> GetTotalAverageReviews([FromRoute]ReviewGetAverageRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewAddRequest command)
        {

          var entity= await mediator.Send(command);
            return Ok(entity);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ReviewEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] ReviewRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
