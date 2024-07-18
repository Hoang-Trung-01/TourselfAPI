using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.Destination;

namespace TourSelfAPI.Controllers
{
    [Route("api/destinations")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DestinationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public static Expression<Func<Destination, object>> GetOrderBy(string orderBy)
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
            Expression<Func<Destination, bool>> filter = p =>
                   (searchValue == null || p.Name.Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.DestinationRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllDestinationResponse>>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var destination = unitOfWork.DestinationRepository.GetByID(id);

            if (destination == null)
                return NotFound("Destination not exist");
            var category = unitOfWork.DestinationRepository.GetByID(destination.Id);
            var result = new Response.DestinationResponse()
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                Address = destination.Address,
                Phone = destination.Phone,
                Email = destination.Email,
                Price = destination.Price,
                Rating = destination.Rating,
                Img = destination.Img,
                PlaceId = destination.PlaceId,
                Url = destination.Url,
                Type = destination.Type,
            };
            var response = mapper.Map<Response.DestinationResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Request.createDestination request)
        {
            Expression<Func<Destination, object>> orderBy = entity => entity.Id;
            var lastProduct = unitOfWork.DestinationRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            var newDestination = new Destination
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                Price = request.Price,
                Rating = request.Rating,
                Img = request.Img,
                PlaceId = request.PlaceId,
                Url = request.Url,
                Type = request.Type,
            };

            unitOfWork.DestinationRepository.Insert(newDestination);
            unitOfWork.Save();
            var response = mapper.Map<Response.DestinationResponse>(newDestination);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromForm] Request.UpdateDestination request)
        {

            var destination = unitOfWork.DestinationRepository.GetByID(request.Id);
            if (destination == null)
            {
                throw new Exception("Destination does not exist");
            }

            destination.Name = request.Name;
            destination.Description = request.Description;
            destination.Address = request.Address;
            destination.Phone = request.Phone;
            destination.Email = request.Email;
            destination.Price = request.Price;
            destination.Rating = request.Rating;
            destination.Img = request.Img;
            destination.PlaceId = request.PlaceId;
            destination.Url = request.Url;
            destination.Type = request.Type;

            unitOfWork.DestinationRepository.Update(destination);
            unitOfWork.Save();

            var response = mapper.Map<Response.DestinationResponse>(destination);
            return Accepted(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var destination = unitOfWork.DestinationRepository.GetByID(id);
            if (destination == null)
            {
                throw new Exception("Destination does not exist");
            }

            unitOfWork.DestinationRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
