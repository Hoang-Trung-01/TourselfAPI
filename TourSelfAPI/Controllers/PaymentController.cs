using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.Payment;

namespace TourSelfAPI.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public static Expression<Func<Payment, object>> GetOrderBy(string orderBy)
           => orderBy?.ToLower() switch
           {
               "name" => e => e.PaymentMethod,
               _ => e => e.PaymentMethod
           };

        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            string? orderBy = "",
            bool? sortOrderAsc = true,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            Expression<Func<Payment, bool>> filter = p =>
                   (searchValue == null || p.PaymentMethod.Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.PaymentRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllPaymentResponse>>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var payment = unitOfWork.PaymentRepository.GetByID(id);

            if (payment == null)
                return NotFound("payment not exist");
            var category = unitOfWork.PaymentRepository.GetByID(payment.Id);
            var result = new Response.PaymentResponse()
            {
                id = payment.Id,
                BookingId = payment.BookingId,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount,
                Date = payment.Date,
                PaymentStatus = payment.PaymentStatus,
                CreatedDate = payment.CreatedDate,
                Status = payment.Status,
            };
            var response = mapper.Map<Response.PaymentResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Request.createPayment request)
        {
            Expression<Func<Payment, object>> orderBy = entity => entity.Id;
            var lastProduct = unitOfWork.PaymentRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            // Parse the date in the "dd/MM/yyyy" format
            DateTime parsedDate;
            bool isDateParsed = DateTime.TryParseExact(request.Date, "dd/MM/yyyy",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None,
                                                       out parsedDate);

            var newPayment = new Payment
            {
                BookingId = request.BookingId,
                PaymentMethod = request.PaymentMethod,
                Amount = request.Amount,
                Date = parsedDate, //use the parsed date
                Status = request.Status,
                PaymentStatus = request.PaymentStatus,
                CreatedDate = DateTime.Now,

            };

            unitOfWork.PaymentRepository.Insert(newPayment);
            unitOfWork.Save();
            var response = mapper.Map<Response.PaymentResponse>(newPayment);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromForm] Request.UpdatePayment request)
        {

            var payment = unitOfWork.PaymentRepository.GetByID(request.id);
            if (payment == null)
            {
                throw new Exception("Payment does not exist");
            }

            // Parse the date in the "dd/MM/yyyy" format
            DateTime parsedDate;
            bool isDateParsed = DateTime.TryParseExact(request.Date, "dd/MM/yyyy",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None,
                                                       out parsedDate);

            payment.BookingId = request.BookingId;
            payment.PaymentMethod = request.PaymentMethod;
            payment.Amount = request.Amount;
            payment.Date = parsedDate; //use the parsed date
            payment.Status = request.Status;
            payment.PaymentStatus = request.PaymentStatus;

            unitOfWork.PaymentRepository.Update(payment);
            unitOfWork.Save();

            var response = mapper.Map<Response.PaymentResponse>(payment);
            return Accepted(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var payment = unitOfWork.PaymentRepository.GetByID(id);
            if (payment == null)
            {
                throw new Exception("Payment does not exist");
            }

            unitOfWork.PaymentRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
