using MediatR;
using Project.Application.Modules.DescriptionModule.Queries;

namespace Project.Application.Modules.DescriptionsModule.Queries.DescriptionGetAllQuery
{
    public class DescriptionGetAllRequest:IRequest<IEnumerable<DescriptionDto>>
    {
    }
}
