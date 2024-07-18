using Domain.Entities;

namespace TourSelfAPI.Services.Payment
{
    public class Request
    {
        public record createPayment
        {
            public int BookingId { get; set; }
            public string PaymentMethod { get; set; }
            public double Amount { get; set; }
            public string Date { get; set; }
            public string Status { get; set; }
            public string PaymentStatus { get; set; }

        }

        public record UpdatePayment
        {
            public int id { get; set; }
            public int BookingId { get; set; }
            public string PaymentMethod { get; set; }
            public double Amount { get; set; }
            public string Date { get; set; }
            public string Status { get; set; }
            public string PaymentStatus { get; set; }
        }
    }
}
