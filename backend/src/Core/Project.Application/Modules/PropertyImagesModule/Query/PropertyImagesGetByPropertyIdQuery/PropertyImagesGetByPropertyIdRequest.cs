using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery
{
    public class PropertyImagesGetByPropertyIdRequest :IRequest<IEnumerable<PropertyImageDetailsDto>>
    {
        public int PropertyId { get; set; }
    }
}
