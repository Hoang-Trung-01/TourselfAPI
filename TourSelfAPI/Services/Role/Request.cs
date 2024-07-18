namespace TourSelfAPI.Services.Role
{
    public class Request
    {
        public record createRole
        {
            public string RoleName { get; set; }
        }

        public record UpdateRole
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }
    }
}
