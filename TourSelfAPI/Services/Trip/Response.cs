namespace TourSelfAPI.Services.Trip
{
    public class Response
    {
        public record GetAllTripResponse
        {
            public int Id { get; set; }
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Time { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
        public record TripResponse
        {
            public int Id { get; set; }
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Time { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
    }
}
