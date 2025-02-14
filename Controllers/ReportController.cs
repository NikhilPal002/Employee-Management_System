using Employee_management.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet("attendance-summary")]
        public async Task<IActionResult> GetAttendanceSummary([FromQuery] string monthYear)
        {
            try
            {
                if (string.IsNullOrEmpty(monthYear))
                    return BadRequest(new { Message = "MonthYear parameter is required." });

                var result = await _reportRepository.GetAttendanceSummary(monthYear);
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest(new { Message = "Invalid MonthYear format. Use YYYY-MM." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving attendance summary.", Error = ex.Message });
            }
        }

        [HttpGet("leave-summary")]
        public async Task<IActionResult> GetLeaveSummary([FromQuery] string monthYear)
        {
            try
            {
                if (string.IsNullOrEmpty(monthYear))
                    return BadRequest(new { Message = "MonthYear parameter is required." });

                var result = await _reportRepository.GetLeaveSummary(monthYear);
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest(new { Message = "Invalid MonthYear format. Use YYYY-MM." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving leave summary.", Error = ex.Message });
            }
        }


        [HttpGet("payroll-summary")]
        public async Task<IActionResult> GetPayrollSummary([FromQuery] string monthYear)
        {
            try
            {
                if (string.IsNullOrEmpty(monthYear))
                    return BadRequest(new { Message = "MonthYear parameter is required." });

                var result = await _reportRepository.GetPayrollSummary(monthYear);
                return Ok(result);
            }
            catch (FormatException)
            {
                return BadRequest(new { Message = "Invalid MonthYear format. Use YYYY-MM." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving payroll summary.", Error = ex.Message });
            }
        }
    }
}
