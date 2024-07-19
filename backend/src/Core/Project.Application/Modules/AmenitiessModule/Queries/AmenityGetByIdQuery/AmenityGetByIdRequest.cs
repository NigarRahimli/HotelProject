using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.AmenitiesModule.Queries.AmenityGetByIdQuery
{
    public class AmenityGetByIdRequest:IRequest<Amenity>
    {
        public int Id { get; set; }
    }
}
