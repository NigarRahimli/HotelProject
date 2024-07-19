using MediatR;

namespace Project.Application.Modules.KindsModule.Queries.KindGetAllQuery
{
    public class KindGetAllRequest:IRequest<IEnumerable<KindDto>>
    {
    }
}
