using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.Role;

namespace TourSelfAPI.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RoleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public static Expression<Func<Role, object>> GetOrderBy(string orderBy)
           => orderBy?.ToLower() switch
           {
               "name" => e => e.RoleName,
               _ => e => e.RoleName
           };

        [HttpGet]
        public IActionResult Get(
            string? searchValue = null,
            string? orderBy = "",
            bool? sortOrderAsc = true,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            Expression<Func<Role, bool>> filter = p =>
                   (searchValue == null || p.RoleName.Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.RoleRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllRoleResponse>>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = unitOfWork.RoleRepository.GetByID(id);

            if (role == null)
                return NotFound("Role not exist");
            var category = unitOfWork.RoleRepository.GetByID(role.RoleId);
            var result = new Response.RoleResponse()
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
            var response = mapper.Map<Response.RoleResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Request.createRole request)
        {
            Expression<Func<Role, object>> orderBy = entity => entity.RoleId;
            var lastProduct = unitOfWork.RoleRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            var newRole = new Role
            {
                RoleName = request.RoleName
            };

            unitOfWork.RoleRepository.Insert(newRole);
            unitOfWork.Save();
            var response = mapper.Map<Response.RoleResponse>(newRole);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromForm] Request.UpdateRole request)
        {

            var role = unitOfWork.RoleRepository.GetByID(request.RoleId);
            if (role == null)
            {
                throw new Exception("Role does not exist");
            }

            role.RoleName = request.RoleName;

            unitOfWork.RoleRepository.Update(role);
            unitOfWork.Save();

            var response = mapper.Map<Response.RoleResponse>(role);
            return Accepted(response);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var role = unitOfWork.RoleRepository.GetByID(id);
            if (role == null)
            {
                throw new Exception("Role does not exist");
            }

            unitOfWork.RoleRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
