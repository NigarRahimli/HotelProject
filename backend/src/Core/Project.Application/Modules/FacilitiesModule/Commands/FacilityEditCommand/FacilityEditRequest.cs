﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityEditCommand
{
    public class FacilityEditRequest : IRequest<Facility>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }

    }
}
