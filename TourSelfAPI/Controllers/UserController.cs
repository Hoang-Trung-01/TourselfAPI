using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.User;

namespace TourSelfAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public static Expression<Func<User, object>> GetOrderBy(string orderBy)
           => orderBy?.ToLower() switch
           {
               "name" => e => e.FullName,
               _ => e => e.FullName
           };
        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            string? orderBy = "",
            bool? sortOrderAsc = true,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            Expression<Func<User, bool>> filter = p =>
                   (searchValue == null || p.FullName.Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.UserRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllUserResponse>>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = unitOfWork.UserRepository.GetByID(id);

            if (user == null)
                return NotFound("User not exist");
            var category = unitOfWork.UserRepository.GetByID(user.Id);
            var result = new Response.UserResponse()
            {
                Id = user.Id,
                FullName = user.FullName,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone,
                Birthday = user.Birthday.ToString("dd-mm-yyyy"),
                Gender = user.Gender,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };
            var response = mapper.Map<Response.UserResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Request.createUser request)
        {
            Expression<Func<User, object>> orderBy = entity => entity.Id;
            var lastProduct = unitOfWork.UserRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            var newUser = new User
            {
                RoleId = request.RoleId,
                FullName = request.FullName,
                Password = request.Password,
                Email = request.Email,
                Phone = request.Phone,
                Birthday = DateTime.ParseExact(request.Birthday.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Gender = request.Gender,
                CreatedAt = DateTime.Now,
                UpdatedAt= DateTime.Now,
            };

            unitOfWork.UserRepository.Insert(newUser);
            unitOfWork.Save();
            var response = mapper.Map<Response.UserResponse>(newUser);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromForm] Request.UpdateUser request)
        {

            var user = unitOfWork.UserRepository.GetByID(request.Id);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            user.RoleId = request.RoleId;
            user.FullName = request.FullName;
            user.Password = request.Password;
            user.Birthday = DateTime.ParseExact(request.Birthday.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            user.Gender = request.Gender;
            user.UpdatedAt = DateTime.Now;

            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();

            var response = mapper.Map<Response.UserResponse>(user);
            return Accepted(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = unitOfWork.UserRepository.GetByID(id);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            unitOfWork.UserRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
