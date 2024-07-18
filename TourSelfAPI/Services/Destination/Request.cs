namespace TourSelfAPI.Services.Destination
{
    public class Request
    {
        public record createDestination
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public double Price { get; set; }
            public double Rating { get; set; }
            public string Img { get; set; }
            public int PlaceId { get; set; }
            public string Url { get; set; }
            public int Type { get; set; }
        }

        public record UpdateDestination
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public double Price { get; set; }
            public double Rating { get; set; }
            public string Img { get; set; }
            public int PlaceId { get; set; }
            public string Url { get; set; }
            public int Type { get; set; }
        }
    }
}
