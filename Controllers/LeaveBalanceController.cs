using AutoMapper;
using Employee_management.Models;
using Employee_management.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employee_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly IMapper _mapper;

        public LeaveBalanceController(ILeaveBalanceRepository leaveBalanceRepository, IMapper mapper)
        {
            _leaveBalanceRepository = leaveBalanceRepository;
            _mapper = mapper;
        }

        // ✅ Get Employee Leave Balance
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetLeaveBalance(int employeeId)
        {
            var leaveBalance = await _leaveBalanceRepository.GetLeaveBalanceAsync(employeeId);
            if (leaveBalance == null)
                return NotFound("Leave balance not found for the employee.");

            // Use AutoMapper to map Entity to DTO
            var leaveBalanceDto = _mapper.Map<LeaveBalanceDto>(leaveBalance);
            return Ok(leaveBalanceDto);
        }

        // ✅ Update Used Leaves when a leave is approved
        // PUT: Update Used Leaves
        [HttpPut("update-used-leaves/{employeeId}")]
        public async Task<IActionResult> UpdateUsedLeaves(int employeeId, [FromBody] UpdateLeaveBalanceDto request)
        {
            var leaveBalance = await _leaveBalanceRepository.GetLeaveBalanceAsync(employeeId);
            if (leaveBalance == null)
                return NotFound("Leave balance not found.");

            // Use AutoMapper to update the entity from DTO
            _mapper.Map(request, leaveBalance);

            var updated = await _leaveBalanceRepository.UpdateLeaveBalanceAsync(leaveBalance);
            if (!updated)
                return BadRequest("Failed to update leave balance.");

            return Ok("Leave balance updated successfully.");
        }

        // ✅ Reset Leave Balance (e.g., at the start of a new year)
        [HttpPut("reset/{employeeId}")]
        public async Task<IActionResult> ResetLeaveBalance(int employeeId)
        {
            var reset = await _leaveBalanceRepository.ResetLeaveBalanceAsync(employeeId);
            if (!reset)
                return BadRequest(new { message = "Failed to reset leave balance." });

            return Ok(new { message = "Leave balance reset successfully." });
        }
    }
}
