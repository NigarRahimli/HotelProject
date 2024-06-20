using MediatR;
using Project.Application.Modules.PropertiesModule.Commands;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Concretes;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyPagedQuery
{
    public class PropertyPagedRequest : Pageable, IPageable, IRequest<IPaginate<PropertyDto>>
    {
    }
}
