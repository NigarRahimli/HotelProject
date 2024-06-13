using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand
{
    public class AmenityEditRequest : IRequest<Amenity>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
