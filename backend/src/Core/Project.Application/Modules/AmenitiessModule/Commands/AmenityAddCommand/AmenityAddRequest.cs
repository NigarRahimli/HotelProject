using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand
{
    public class AmenityAddRequest: IRequest<Amenity>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
