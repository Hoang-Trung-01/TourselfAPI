namespace TourSelfAPI.Services.Plan
{
    public class Request
    {
        public record createPlan
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int DurationDate { get; set; }
            public double TotalCost { get; set; }
            public int PlaceId { get; set; }
            public string Status { get; set; }
        }

        public record UpdatePlan
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int DurationDate { get; set; }
            public double TotalCost { get; set; }
            public int PlaceId { get; set; }
            public string Status { get; set; }
        }
    }
}
