namespace TourSelfAPI.Services.Booking
{
    public class Request
    {
        public record createBooking
        {
            public int UserId { get; set; }
            public int PlanId { get; set; }
            public int PaymentId { get; set; }
            public string Date { get; set; }
            public double TotalPrice { get; set; }
            public string Status { get; set; }
        }

        public record UpdateBooking
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int PlanId { get; set; }
            public int PaymentId { get; set; }
            public string Date { get; set; }
            public double TotalPrice { get; set; }
            public string Status { get; set; }
        }
    }
}
