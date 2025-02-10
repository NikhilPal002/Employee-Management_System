using AutoMapper;
using Employee_management.Models;
using Employee_management.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Employee_management.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository leaveRepository;
        private readonly IMapper _mapper;

        public LeaveController(ILeaveRepository leaveRepository, IMapper mapper)
        {
            this.leaveRepository = leaveRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaves()
        {
            var leaves = await leaveRepository.GetAllLeaveAsync();

            if (leaves == null || !leaves.Any())
            {
                return NotFound(new { message = "No leaves found." });
            }

            var leavesDto = _mapper.Map<List<LeaveDto>>(leaves);
            return Ok(leavesDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveById(int id)
        {
            var leave = await leaveRepository.GetLeaveByIdAsync(id);

            if (leave == null)
                return NotFound(new { message = "Leave request not found" });

            var leaveDto = _mapper.Map<LeaveDto>(leave);
            return Ok(leaveDto);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyLeave([FromBody] AddLeaveDto addLeaveDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (addLeaveDto.StartDate >= addLeaveDto.EndDate)
                return BadRequest("Start Date must be before End Date.");

            var leave = _mapper.Map<Leave>(addLeaveDto);

            await leaveRepository.ApplyLeaveAsync(leave);

            var createdLeave = _mapper.Map<LeaveDto>(leave);

            return CreatedAtAction(nameof(GetLeaves), new { id = createdLeave.LeaveID }, _mapper.Map<LeaveDto>(createdLeave));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeave(int id, UpdateLeaveDto leaveDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != leaveDto.LeaveID)
                return BadRequest(new { message = "Leave not found" });

            var leave = _mapper.Map<Leave>(leaveDto);

            var result = await leaveRepository.UpdateLeaveAsync(id, leave);

            if (!result)
                return BadRequest(new { message = "Cannot update leave once it is approved or rejected" });

            return NoContent();
        }


        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateLeaveStatus(int id, [FromBody] UpdateLeaveStatusDto request)
        {
            var result = await leaveRepository.UpdateLeaveStatusAsync(id, request);

            if (result == null)
                return NotFound(new { message = "Leave not found" });

            if (!result)
                return BadRequest(new { message = "Leave has already been processed or not found" });

            return NoContent();
        }

        // ✅ Delete Leave Request (Only if pending)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            var result = await leaveRepository.DeleteLeaveAsync(id);
            if (result == null)
                return NotFound(new { message = "Leave not found" });

            if (!result)
                return BadRequest(new { message = "Cannot delete approved or rejected leave" });

            return NoContent();
        }

    }
}