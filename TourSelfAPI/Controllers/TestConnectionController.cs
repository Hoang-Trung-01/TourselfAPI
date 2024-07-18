using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSefl.Repository;

namespace TourSelfAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestConnectionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestConnectionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Kiểm tra kết nối cơ sở dữ liệu bằng cách truy vấn đơn giản
                var test = await _context.Roles.FirstOrDefaultAsync();
                return Ok("Connection successful");
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi chi tiết
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
