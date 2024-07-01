using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.LocationsModule.Commands.LocationAddCommand
{
    public class LocationAddRequest : IRequest<Location>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
