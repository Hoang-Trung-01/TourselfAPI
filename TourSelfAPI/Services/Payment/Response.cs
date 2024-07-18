namespace TourSelfAPI.Services.Payment
{
    public class Response
    {
        public record GetAllPaymentResponse
        {
            public int BookingId { get; set; }
            public string PaymentMethod { get; set; }
            public double Amount { get; set; }
            public DateTime Date { get; set; }
            public string Status { get; set; }
            public string PaymentStatus { get; set; }
            public DateTime CreatedDate { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }

        public record PaymentResponse
        {
            public int id { get; set; }
            public int BookingId { get; set; }
            public string PaymentMethod { get; set; }
            public double Amount { get; set; }
            public DateTime Date { get; set; }
            public string Status { get; set; }
            public string PaymentStatus { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}
