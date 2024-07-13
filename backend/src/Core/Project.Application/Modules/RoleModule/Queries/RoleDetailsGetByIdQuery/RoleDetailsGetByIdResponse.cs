using Project.Application.Modules.RoleModule.Queries;

namespace Project.Application.Modules.RoleModule.Queries.RoleDetailsGetByIdQuery
{
    public class RoleDetailsGetByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PolicyDto> Policies { get; set; }
        public IEnumerable<RoleMemberDto> Members { get; set; }
    }
}
