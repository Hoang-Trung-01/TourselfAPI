using System.Windows.Input;

namespace TourSelfAPI.Services.Place
{
    public class Request
    {
        public record createPlace
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string Img { get; set; }
            public int TripId { get; set; }

        }
        public record UpdatePlace 
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string Img { get; set; }
            public int TripId { get; set; }

        }
    }
}
