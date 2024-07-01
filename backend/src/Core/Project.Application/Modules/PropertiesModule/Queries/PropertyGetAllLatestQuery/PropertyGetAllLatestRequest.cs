using MediatR;


namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllLatestQuery
{
    public class PropertyGetAllLatestRequest : IRequest<IEnumerable<PropertyWithHeartDto>>
    {
        public int Take { get; set; }
    }
}
