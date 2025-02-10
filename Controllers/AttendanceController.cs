using AutoMapper;
using Employee_management.Models;
using Employee_management.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Employee_management.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceRepository attendanceRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }


        // ✅ Get All Attendance Records
        [HttpGet]
        public async Task<ActionResult> GetAttendances()
        {
            var attendances = await _attendanceRepository.GetAllAttendanceRecordsAsync();

            var attendanceDto = _mapper.Map<List<AttendanceDto>>(attendances);
            return Ok(attendanceDto);
        }

        // ✅ Get Attendance by Employee ID
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult> GetEmployeeAttendance(int employeeId)
        {
            var attendances = await _attendanceRepository.GetEmployeeAttendanceAsync(employeeId);

            if (!attendances.Any())
                return NotFound(new { message = "No attendance records found" });

            var attendanceDto = _mapper.Map<List<AttendanceDto>>(attendances);
            return Ok(attendanceDto);
        }

        // ✅ Check-in (Start Work)
        [HttpPost("checkin")]
        public async Task<ActionResult> CheckIn([FromBody] CheckInDto request)
        {
            try
            {
                var attendance = _mapper.Map<Attendance>(request);
                attendance.Date = DateTime.Now.Date;
                attendance.CheckInTime = DateTime.Now.TimeOfDay;

                var result = await _attendanceRepository.CheckInAsync(attendance);
                var attendanceDto = _mapper.Map<AttendanceDto>(result);

                return CreatedAtAction(nameof(GetEmployeeAttendance), new { employeeId = attendance.EmployeeID }, attendanceDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }

        // ✅ Check-out (End Work)
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDto request)
        {
            var success = await _attendanceRepository.CheckOutAsync(request.EmployeeID);

            if (!success)
                return NotFound(new { message = "No check-in record found for today or already checked out" });

            return NoContent();
        }
    }
}