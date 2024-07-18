using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.Trip;

namespace TourSelfAPI.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TripController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public static Expression<Func<Trip, object>> GetOrderBy(string orderBy)
           => orderBy?.ToLower() switch
           {
               "name" => e => e.Name,
               _ => e => e.Name
           };

        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            string? orderBy = "",
            bool? sortOrderAsc = true,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            Expression<Func<Trip, bool>> filter = p =>
                   (searchValue == null || p.Name.Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.TripRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllTripResponse>>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var trip = unitOfWork.TripRepository.GetByID(id);

            if (trip == null)
                return NotFound("Trip not exist");
            var category = unitOfWork.TripRepository.GetByID(trip.Id);
            var result = new Response.TripResponse()
            {
                Id = trip.Id,
                PlaceId = trip.PlaceId,
                Name = trip.Name,
                Date = trip.Date,
                Time = trip.Time
            };
            var response = mapper.Map<Response.TripResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Request.createTrip request)
        {
            Expression<Func<Trip, object>> orderBy = entity => entity.Id;
            var lastProduct = unitOfWork.TripRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            // Parse the date in the "dd/MM/yyyy" format
            DateTime parsedDate;
            bool isDateParsed = DateTime.TryParseExact(request.Date, "dd/MM/yyyy",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None,
                                                       out parsedDate);

            var newTrip = new Trip
            {
                PlaceId = request.PlaceId,
                Name = request.Name,
                Date = parsedDate,
                Time = request.Time
            };

            unitOfWork.TripRepository.Insert(newTrip);
            unitOfWork.Save();
            var response = mapper.Map<Response.TripResponse>(newTrip);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromForm] Request.UpdateTrip request)
        {

            var trip = unitOfWork.TripRepository.GetByID(request.Id);
            if (trip == null)
            {
                throw new Exception("Trip does not exist");
            }

            // Parse the date in the "dd/MM/yyyy" format
            DateTime parsedDate;
            bool isDateParsed = DateTime.TryParseExact(request.Date, "dd/MM/yyyy",
                                                       CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None,
                                                       out parsedDate);

            trip.PlaceId = request.PlaceId;
            trip.Name = request.Name;
            trip.Date = parsedDate;
            trip.Time = request.Time;

            unitOfWork.TripRepository.Update(trip);
            unitOfWork.Save();

            var response = mapper.Map<Response.TripResponse>(trip);
            return Accepted(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var place = unitOfWork.TripRepository.GetByID(id);
            if (place == null)
            {
                throw new Exception("Trip does not exist");
            }

            unitOfWork.TripRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
