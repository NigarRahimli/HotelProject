using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand
{
    public class AmenityAddRequest: IRequest<Amenity>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
