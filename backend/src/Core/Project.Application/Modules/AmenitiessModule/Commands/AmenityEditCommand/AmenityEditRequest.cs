using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand
{
    public class AmenityEditRequest : IRequest<Amenity>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
