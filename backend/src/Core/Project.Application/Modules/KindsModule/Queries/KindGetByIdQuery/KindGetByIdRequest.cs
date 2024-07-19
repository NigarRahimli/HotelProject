using MediatR;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.KindsModule.Queries.KindGetByIdQuery
{
    public class KindGetByIdRequest:IRequest<Kind>
    {
        public int Id { get; set; }
    }
}
