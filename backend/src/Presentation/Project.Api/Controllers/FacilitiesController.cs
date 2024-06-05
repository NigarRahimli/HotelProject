﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.FacilitiesModule.Commands.FacilityAddCommand;
using Project.Application.Modules.FacilitiesModule.Commands.FacilityEditCommand;
using Project.Application.Modules.FacilitiesModule.Commands.FacilityRemoveCommand;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetAllQuery;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByIdQuery;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public FacilitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] FacilityGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] FacilityGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Add(FacilityAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] FacilityEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] FacilityRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
