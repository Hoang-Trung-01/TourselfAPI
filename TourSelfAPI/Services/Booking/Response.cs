namespace TourSelfAPI.Services.Booking
{
    public class Response
    {
        public record GetAllBookingResponse
        {
            public int UserId { get; set; }
            public int PlanId { get; set; }
            public int PaymentId { get; set; }
            public string Date { get; set; }
            public double TotalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Status { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
        public record BookingResponse
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int PlanId { get; set; }
            public int PaymentId { get; set; }
            public string Date { get; set; }
            public double TotalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Status { get; set; }
        }
    }
}
