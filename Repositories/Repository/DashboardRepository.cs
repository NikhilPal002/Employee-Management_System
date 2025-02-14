using Employee_management.data;
using Employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly EmployeeDbContext _context;

        public DashboardRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeOverviewDto> GetEmployeesOverview()
        {
            try
            {
                var totalEmployees = await _context.Employees.CountAsync();

                // Check active employees based on payroll records
                var activeEmployees = await _context.Employees
                    .CountAsync(e => _context.Payrolls.Any(p => p.EmployeeID == e.EmployeeID));

                var newHiresThisMonth = await _context.Employees
                    .CountAsync(e => e.JoinDate.Month == DateTime.Now.Month && e.JoinDate.Year == DateTime.Now.Year);

                return new EmployeeOverviewDto
                {
                    TotalEmployees = totalEmployees,
                    ActiveEmployees = activeEmployees,
                    NewHiresThisMonth = newHiresThisMonth
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching employee overview", ex);
            }
        }



        public async Task<DashboardStatisticsDto> GetDashboardStatistics()
        {
            try
            {
                var totalEmployees = await _context.Employees.AsNoTracking().CountAsync();

                var employeesPresentToday = await _context.Attendance.AsNoTracking()
                    .CountAsync(a => a.Date.Date == DateTime.Today && a.CheckInTime != null);

                var employeesOnLeaveToday = await _context.Leaves.AsNoTracking()
                    .CountAsync(l => l.StartDate.Date <= DateTime.Today && l.EndDate.Date >= DateTime.Today);

                // Get the current month-year as a string
                string currentMonthYear = $"{DateTime.Now.Year}-{DateTime.Now.Month:D2}";

                var totalPayrollPaidThisMonth = await _context.Payrolls.AsNoTracking()
                    .Where(p => p.MonthYear == currentMonthYear) // Use precomputed string
                    .SumAsync(p => (decimal?)p.NetSalary) ?? 0m;

                var pendingLeaveRequests = await _context.Leaves.AsNoTracking()
                    .CountAsync(l => l.Status == "Pending");

                return new DashboardStatisticsDto
                {
                    TotalEmployees = totalEmployees,
                    EmployeesPresentToday = employeesPresentToday,
                    EmployeesOnLeaveToday = employeesOnLeaveToday,
                    TotalPayrollPaidThisMonth = totalPayrollPaidThisMonth,
                    PendingLeaveRequests = pendingLeaveRequests
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving dashboard statistics.", ex);
            }
        }

    }
}