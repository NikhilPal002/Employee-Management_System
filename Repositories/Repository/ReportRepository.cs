using Employee_management.data;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly EmployeeDbContext _context;

        public ReportRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttendanceSummaryDto>> GetAttendanceSummary(string monthYear)
        {
            try
            {
                var parsedDate = DateTime.Parse(monthYear);

                var attendanceData = await _context.Attendance
                    .Where(a => a.Date.Month == parsedDate.Month && a.Date.Year == parsedDate.Year)
                    .GroupBy(a => a.EmployeeID)
                    .Select(g => new AttendanceSummaryDto
                    {
                        EmployeeId = g.Key,
                        Name = _context.Employees.Where(e => e.EmployeeID == g.Key).Select(e => e.Name).FirstOrDefault(),
                        PresentDays = g.Count(a => a.CheckInTime != null && a.CheckOutTime != null),
                        AbsentDays = g.Count(a => a.CheckInTime == null && a.CheckOutTime == null),
                        LeaveDays = _context.Leaves.Count(l => l.EmployeeID == g.Key &&
                                                               l.StartDate.Month == parsedDate.Month &&
                                                               l.StartDate.Year == parsedDate.Year),
                        AttendancePercentage = (g.Count(a => a.CheckInTime != null && a.CheckOutTime != null) * 100.0) / g.Count()
                    })
                    .ToListAsync();

                return attendanceData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LeaveSummaryDto>> GetLeaveSummary(string monthYear)
        {
            try
            {
                var parsedDate = DateTime.Parse(monthYear);

                var leaveData = await _context.Leaves
                    .Where(l => l.StartDate.Month == parsedDate.Month && l.StartDate.Year == parsedDate.Year)
                    .GroupBy(l => l.EmployeeID)
                    .Select(g => new LeaveSummaryDto
                    {
                        EmployeeId = g.Key,
                        Name = _context.Employees.Where(e => e.EmployeeID == g.Key).Select(e => e.Name).FirstOrDefault(),
                        TotalLeaves = g.Count(),
                        PaidLeaves = g.Count(l => l.LeaveType.ToLower().Contains("paid")),
                        UnpaidLeaves = g.Count(l => !l.LeaveType.ToLower().Contains("paid")),
                        PendingPaidLeaves = _context.EmployeeLeaveBalances
                            .Where(b => b.EmployeeID == g.Key)
                            .Select(b => b.PendingPaidLeaves)
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                return leaveData;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<PayrollSummaryDto>> GetPayrollSummary(string monthYear)
        {
            try
            {
                return await _context.Payrolls
                    .Where(p => p.MonthYear == monthYear)
                    .Select(p => new PayrollSummaryDto
                    {
                        EmployeeId = p.EmployeeID,
                        Name = _context.Employees.Where(e => e.EmployeeID == p.EmployeeID).Select(e => e.Name).FirstOrDefault(),
                        BasicSalary = p.BasicSalary,
                        Deductions = p.Deductions,
                        NetSalary = p.NetSalary
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}