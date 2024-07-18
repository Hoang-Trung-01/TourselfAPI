namespace TourSelfAPI.Services.User
{
    public class Response
    {
        public record GetAllUserResponse
        {
            public int RoleId { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Phone {  get; set; }
            public string Birthday { get; set; }
            public string Gender { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
        public record UserResponse
        {
            public int Id { get; set; }
            public int RoleId { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Birthday { get; set; }
            public string Gender { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
}
