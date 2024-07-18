namespace TourSelfAPI.Services.Trip
{
    public class Request
    {
        public record createTrip
        {
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
        }

        public record UpdateTrip
        {
            public int Id { get; set; }
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
        }
    }
}
