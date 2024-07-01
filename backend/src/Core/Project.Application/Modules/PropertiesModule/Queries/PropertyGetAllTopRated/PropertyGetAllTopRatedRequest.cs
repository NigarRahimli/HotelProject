using MediatR;


namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllTopRated
{
    public class PropertyGetAllTopRatedRequest : IRequest<IEnumerable<PropertyWithHeartAndRateDto>>
    {
        public int Take { get; set; }
    }
}
