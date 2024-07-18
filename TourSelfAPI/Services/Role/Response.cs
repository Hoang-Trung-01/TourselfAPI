namespace TourSelfAPI.Services.Role
{
    public class Response
    {
        public record GetAllRoleResponse
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
        public record RoleResponse
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
    }
}
