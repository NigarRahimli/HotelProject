using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.TransactionModule.Commands.ProcessPaymentCommand;



namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }




       
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] ProcessPaymentRequest request)
        {
            var result =await mediator.Send(request);
            return Ok(result);
        }

    }
}
