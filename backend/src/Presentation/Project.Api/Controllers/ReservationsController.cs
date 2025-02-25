﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Modules.ReservationModule.Commands.ReservationAddCommand;
using Project.Application.Modules.ReservationModule.Commands.ReservationChangeStatusCommand;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetAllByPropertyIdQuery;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetAllQuery;
using Project.Application.Modules.ReservationModule.Queries.ReservationGetByIdQuery;
using Project.Application.Modules.ReservationsModule.Commands.ReservationEditCommand;
using Project.Application.Modules.ReservationsModule.Commands.ReservationRemoveCommand;


namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReservationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] ReservationGetByIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("reservations.getbypropid")]
        [HttpGet("property/{propertyId:int:min(1)}")]
        public async Task<IActionResult> GetByPropertyId([FromRoute] ReservationGetAllByPropertyIdRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize("reservations.getbypropid")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] ReservationGetAllRequest request)
        {
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ReservationAddRequest request)
        {

            var entity = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { entity.Id }, entity);
        }
        [Authorize("reservations.changestatus")]
        [HttpPut("change-status/{reservationId:int:min(1)}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int reservationId, [FromBody] ReservationChangeStatusRequest request)
        {
            request.ReservationId = reservationId;
            await mediator.Send(request);
            return Ok ("Reservation status changed successfully");
        }

        [Authorize]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ReservationEditRequest request)
        {
            request.Id = id;
            var entity = await mediator.Send(request);
            return Ok(entity);
        }

        [Authorize]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] ReservationRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }

    }
}
