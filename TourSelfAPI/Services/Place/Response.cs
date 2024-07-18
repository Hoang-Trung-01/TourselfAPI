namespace TourSelfAPI.Services.Place
{
    public class Response
    {
        public record GetAllPlaceResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string Img { get; set; }
            public int RestaurantId { get; set; }
            public int TripId { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
        public record PlaceResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string Img { get; set; }
            public int RestaurantId { get; set; }
            public int TripId { get; set; }
        }
    }
}
