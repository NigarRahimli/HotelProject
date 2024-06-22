using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetByIdQuery
{
    public class DescriptionGetByIdRequest:IRequest<Description>
    {
        public int Id { get; set; }
    }
}
