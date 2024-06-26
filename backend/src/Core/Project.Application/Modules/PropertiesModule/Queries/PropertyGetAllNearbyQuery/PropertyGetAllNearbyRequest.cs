using MediatR;


namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery
{
    public class PropertyGetAllNearbyRequest : IRequest<IEnumerable<PropertyWithHeartDto>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Take { get; set; }
    }
}
