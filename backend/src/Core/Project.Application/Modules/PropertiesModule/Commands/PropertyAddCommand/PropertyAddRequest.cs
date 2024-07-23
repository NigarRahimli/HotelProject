using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.PropertiesModule.Commands.PropertyAddCommand
{
    public class PropertyAddRequest: IRequest<Property>
    {
        public string Name { get; set; }
        public int GuestNum { get; set; }
        public int DescriptionId { get; set; }
        public bool IsPetFriendly { get; set; }
        public int KindId { get; set; }
        public int LocationId { get; set; }
        public float MaxPrice { get; set; }
        public float MedPrice { get; set; }
        public float MinPrice { get; set; }
        
    }
}
