
using Employee_management.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Employee_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("employees-overview")]
        public async Task<IActionResult> GetEmployeesOverview()
        {
            try
            {
                var result = await _dashboardRepository.GetEmployeesOverview();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving employee overview.", Error = ex.Message });
            }
        }


        [HttpGet("statistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            try
            {
                var result = await _dashboardRepository.GetDashboardStatistics();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving dashboard statistics.", Error = ex.Message });
            }
        }
    }

}