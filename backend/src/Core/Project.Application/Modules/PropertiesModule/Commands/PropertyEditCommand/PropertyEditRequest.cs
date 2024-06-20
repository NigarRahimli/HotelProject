
using MediatR;
using Project.Domain.Models.Entities;



namespace Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand
{
    public class PropertyEditRequest : IRequest<Property>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GuestNum { get; set; }
        public int DescriptionId { get; set; }
        public int LocationId { get; set; }
        public bool IsPetFriendly { get; set; }
        public int KindId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
