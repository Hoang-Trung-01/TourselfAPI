namespace TourSelfAPI.Services.Destination
{
    public class Response
    {
        public record GetAllDestinationResponse
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
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }

        public record DestinationResponse
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
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
    }
}
