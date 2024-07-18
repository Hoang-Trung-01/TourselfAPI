namespace TourSelfAPI.Services.User
{
    public class Request
    {
        public record createUser
        {
            public int RoleId { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Birthday { get; set; }
            public string Gender { get; set; }

        }
        public record UpdateUser
        {
            public int Id { get; set; }
            public int RoleId { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Birthday { get; set; }
            public string Gender { get; set; }
        }
    }
}
