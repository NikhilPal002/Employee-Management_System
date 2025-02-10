using Employee_management.Models;

namespace Employee_management.Repositories
{
    public interface ILeaveBalanceRepository
    {
        Task<EmployeeLeaveBalance> GetLeaveBalanceAsync(int employeeId);

        Task<bool> UpdateLeaveBalanceAsync(EmployeeLeaveBalance leaveBalance);

        Task<bool> ResetLeaveBalanceAsync(int employeeId);
    }
}