using MediatR;


namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByPropertyIdQuery
{
    public class FacilityGetByPropertyIdRequest : IRequest<IEnumerable<FacilityDetailDto>>
    {
        public int PropertyId { get; set; }
    }
}
