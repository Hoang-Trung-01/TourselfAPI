using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.Place;

namespace TourSelfAPI.Controllers
{
    [Route("api/places")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlaceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public static Expression<Func<Place, object>> GetOrderBy(string orderBy)
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
            Expression<Func<Place, bool>> filter = p =>
                   (searchValue == null || p.Name.Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);
            
            var result = unitOfWork.PlaceRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllPlaceResponse>>(result);

            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var place = unitOfWork.PlaceRepository.GetByID(id);

            if (place == null)
                return NotFound("place not exist");
            var category = unitOfWork.PlaceRepository.GetByID(place.Id);
            var result = new Response.PlaceResponse()
            {
                Id = place.Id,
                Name = place.Name,
                Description = place.Description,
                Img = place.Img,
                Location = place.Location,
                TripId = place.TripId
            };
            var response = mapper.Map<Response.PlaceResponse>(result);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create([FromForm]  Request.createPlace request)
        {
            Expression<Func<Place, object>> orderBy = entity => entity.Id;
            var lastProduct = unitOfWork.PlaceRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            var newPlace = new Place
            {
                Name = request.Name,
                Description= request.Description,
                Img= request.Img,
                Location = request.Location,
                TripId = request.TripId
            };

            unitOfWork.PlaceRepository.Insert(newPlace);
            unitOfWork.Save();
            var response = mapper.Map<Response.PlaceResponse>(newPlace);
            return Ok(response);
        }


        [HttpPut]
        public IActionResult Update([FromForm ] Request.UpdatePlace request)
        {
           
            var place = unitOfWork.PlaceRepository.GetByID(request.id);
            if(place == null)
            {
                throw new Exception("Place does not exist");
            }

            place.Name = request.Name;
            place.Description = request.Description;
            place.Img = request.Img;
            place.Location = request.Location;
            place.TripId = request.TripId;  

            unitOfWork.PlaceRepository.Update(place);
            unitOfWork.Save();

            var response = mapper.Map<Response.PlaceResponse>(place);
            return Accepted(response);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var place = unitOfWork.PlaceRepository.GetByID(id);
            if (place == null)
            {
                throw new Exception("Place does not exist");
            }

            unitOfWork.PlaceRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
