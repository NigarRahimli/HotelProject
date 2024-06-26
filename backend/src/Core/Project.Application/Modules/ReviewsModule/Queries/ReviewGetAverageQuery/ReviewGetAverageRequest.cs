using MediatR;


namespace Project.Application.Modules.ReviewsModule.Queries.ReviewGetAverageQuery
{
    public class ReviewGetAverageRequest : IRequest<double>
    {
        public int PropertyId { get; set; }
    }
}
