using System.Data.SqlTypes;

namespace TourSelfAPI.Services.Plan
{
    public class Response
    {
        public record GetAllPlanResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int DurationDate { get; set; }
            public double TotalCost { get; set; }
            public DateTime CreatedAt { get; set; }
            public int PlaceId { get; set; }
            public string Status { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }

        public record PlanResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int DurationDate { get; set; }
            public double TotalCost { get; set; }
            public DateTime CreatedAt { get; set; }
            public int PlaceId { get; set; }
            public string Status { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
    }
}
