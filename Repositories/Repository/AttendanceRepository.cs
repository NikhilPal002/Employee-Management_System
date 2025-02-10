using Employee_management.data;
using Employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly EmployeeDbContext _context;

        public AttendanceRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Attendance> CheckInAsync(Attendance attendance)
        {
            if (attendance == null)
                throw new ArgumentNullException(nameof(attendance), "Attendance data cannot be null.");

            var today = DateTime.Now.Date;

            // Check if the employee has already checked in today
            var existingAttendance = await _context.Attendance
                .FirstOrDefaultAsync(a => a.EmployeeID == attendance.EmployeeID && a.Date == today);

            if (existingAttendance != null)
                throw new InvalidOperationException("Employee has already checked in today.");

            // Set Date and Check-In Time
            attendance.Date = today;
            attendance.CheckInTime = DateTime.Now.TimeOfDay;

            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            return attendance;
        }


        public async Task<bool> CheckOutAsync(int employeeId)
        {
            var today = DateTime.Now.Date;
            var attendance = await _context.Attendance
                .FirstOrDefaultAsync(a => a.EmployeeID == employeeId && a.Date == today);

            if (attendance == null || attendance.CheckOutTime != null)
                return false; // No check-in record found or already checked out

            attendance.CheckOutTime = DateTime.Now.TimeOfDay;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Attendance>> GetAllAttendanceRecordsAsync()
        {
            return await _context.Attendance.Include(a => a.Employee).ToListAsync();

        }

        public async Task<List<Attendance>> GetEmployeeAttendanceAsync(int employeeId)
        {
            return await _context.Attendance
            .Where(a => a.EmployeeID == employeeId)
            .ToListAsync();
        }
    }
}