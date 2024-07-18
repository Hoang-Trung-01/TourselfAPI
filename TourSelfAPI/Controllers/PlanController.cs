using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TourSefl.Repository.Interface;
using TourSelfAPI.Services.Plan;

namespace TourSelfAPI.Controllers
{
    [Route("api/plans")]
    [ApiController]
    public class PlanController : Controller
    {
        
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public static Expression<Func<Plan, object>> GetOrderBy(string orderBy)
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
            Expression<Func<Plan, bool>> filter = p =>
                   (searchValue == null || p.Name.Contains(searchValue));

            var keySelector = GetOrderBy(orderBy);

            var result = unitOfWork.PlanRepository.Get(
                    filter: filter,
                    orderBy: keySelector,
                    sortOrderAsc: sortOrderAsc,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                   );

            var response = mapper.Map<IEnumerable<Response.GetAllPlanResponse>>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var plan = unitOfWork.PlanRepository.GetByID(id);

            if (plan == null)
                return NotFound("Plan not exist");
            var category = unitOfWork.PlanRepository.GetByID(plan.Id);
            var result = new Response.PlanResponse()
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDate = plan.DurationDate,
                TotalCost = plan.TotalCost,
                CreatedAt = plan.CreatedAt,
                PlaceId = plan.PlaceId,
                Status = plan.Status
            };
            var response = mapper.Map<Response.PlanResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Request.createPlan request)
        {
            Expression<Func<Plan, object>> orderBy = entity => entity.Id;
            var lastProduct = unitOfWork.PlanRepository.
                Get(orderBy: orderBy, sortOrderAsc: false)
                .FirstOrDefault();

            var newPlan = new Plan
            {
                Name = request.Name,
                Description = request.Description,
                DurationDate = request.DurationDate,
                TotalCost = request.TotalCost,
                CreatedAt = DateTime.Now,
                PlaceId = request.PlaceId,
                Status = request.Status
            };

            unitOfWork.PlanRepository.Insert(newPlan);
            unitOfWork.Save();
            var response = mapper.Map<Response.PlanResponse>(newPlan);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromForm] Request.UpdatePlan request)
        {

            var plan = unitOfWork.PlanRepository.GetByID(request.id);
            if (plan == null)
            {
                throw new Exception("Plan does not exist");
            }

            plan.Name = request.Name;
            plan.Description = request.Description;
            plan.DurationDate = request.DurationDate;
            plan.TotalCost = request.TotalCost;
            plan.PlaceId = request.PlaceId;
            plan.Status = request.Status;

            unitOfWork.PlanRepository.Update(plan);
            unitOfWork.Save();

            var response = mapper.Map<Response.PlanResponse>(plan);
            return Accepted(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var plan = unitOfWork.PlanRepository.GetByID(id);
            if (plan == null)
            {
                throw new Exception("Plan does not exist");
            }

            unitOfWork.PlanRepository.Delete(id);
            unitOfWork.Save();
            return Accepted();
        }
    }
}
