using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityAddCommand
{
    public class FacilityAddRequest: IRequest<Facility>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
