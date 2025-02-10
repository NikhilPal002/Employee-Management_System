using Employee_management.Models;

namespace Employee_management.Repositories
{
    public interface ILeaveBalanceRepository
    {
        Task<EmployeeLeaveBalance> GetLeaveBalanceAsync(int employeeId);

        Task UpdateLeaveBalanceAsync(int employeeId, int usedLeaves);
    }
}