using MediatR;

namespace Project.Application.Modules.RoleModule.Queries.RoleDetailsGetByIdQuery
{
    public class RoleDetailsGetByIdRequest : IRequest<RoleDetailsGetByIdResponse>
    {
        public int Id { get; set; }
        public string[] Policies { get; set; }
        public RoleDetailsGetByIdRequest()
        {
            Policies = new string[0];
        }
    }
}
