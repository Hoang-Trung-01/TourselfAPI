using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.Booking;

namespace TourSelfAPI.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public BookingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public static Expression<Func<Booking, object>> GetOrderBy(string orderBy)
           => orderBy?.ToLower() switch
           {
               "created_date" => e => e.CreatedAt,
               _ => e => e.CreatedAt.ToString(),
           };
        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            string? orderBy = "",
            bool? sortOrderAsc = true,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            Expression<Func<Booking, bool>> filter = b =>
                   (searchValue == null || b.CreatedAt.ToString().Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.BookingRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllBookingResponse>>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var booking = unitOfWork.BookingRepository.GetByID(id);

            if (booking == null)
                return NotFound("Booking not exist");
            var category = unitOfWork.BookingRepository.GetByID(booking.Id);
            var result = new Response.BookingResponse()
            {
                Id = booking.Id,
                UserId = booking.UserId,
                PlanId = booking.PlanId,
                PaymentId = booking.PaymentId,
                Date = booking.Date.ToString("dd-mm-yyyy"),
                TotalPrice = booking.TotalPrice,
                CreatedAt = booking.CreatedAt,
                Status = booking.Status,
            };
            var response = mapper.Map<Response.BookingResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Request.createBooking request)
        {
            Expression<Func<Booking, object>> orderBy = entity => entity.Id;
            var lastProduct = unitOfWork.BookingRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            // Parse the date in the "dd/MM/yyyy" format
            DateTime parsedDate = DateTime.ParseExact(request.Date.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var newBooking = new Booking
            {
                UserId = request.UserId,
                PlanId = request.PlanId,
                PaymentId = request.PaymentId,
                Date = parsedDate,
                TotalPrice = request.TotalPrice,
                CreatedAt = DateTime.Now,
                Status = request.Status,
            };

            unitOfWork.BookingRepository.Insert(newBooking);
            unitOfWork.Save();
            var response = mapper.Map<Response.BookingResponse>(newBooking);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromForm] Request.UpdateBooking request)
        {

            var booking = unitOfWork.BookingRepository.GetByID(request.Id);
            if (booking == null)
            {
                throw new Exception("Booking does not exist");
            }

            // Parse the date in the "dd/MM/yyyy" format
            DateTime parsedDate = DateTime.ParseExact(request.Date.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);


            booking.UserId = request.UserId;
            booking.PlanId = request.PlanId;
            booking.PaymentId = request.PaymentId;
            booking.Date = parsedDate;
            booking.TotalPrice = request.TotalPrice;
            booking.Status = request.Status;

            unitOfWork.BookingRepository.Update(booking);
            unitOfWork.Save();

            var response = mapper.Map<Response.BookingResponse>(booking);
            return Accepted(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = unitOfWork.BookingRepository.GetByID(id);
            if (booking == null)
            {
                throw new Exception("Booking does not exist");
            }

            unitOfWork.BookingRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
