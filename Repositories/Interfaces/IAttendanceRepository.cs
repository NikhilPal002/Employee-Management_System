using Employee_management.Models;

namespace Employee_management.Repositories
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAllAttendanceRecordsAsync();
        Task<List<Attendance>> GetEmployeeAttendanceAsync(int employeeId);
        Task<Attendance> CheckInAsync(Attendance attendance);
        Task<bool> CheckOutAsync(int employeeId);
    }
}