using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand;

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
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewAddRequest command)
        {

          var entity= await mediator.Send(command);
            return Ok(entity);
        }

    }
}
