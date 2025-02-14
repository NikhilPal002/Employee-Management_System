using System.Collections.Generic;
using System.Threading.Tasks;
namespace Employee_management.Repositories
{
    public interface IReportRepository
    {
        Task<List<AttendanceSummaryDto>> GetAttendanceSummary(string monthYear);
        Task<List<LeaveSummaryDto>> GetLeaveSummary(string monthYear);
        Task<List<PayrollSummaryDto>> GetPayrollSummary(string monthYear);
    }
}