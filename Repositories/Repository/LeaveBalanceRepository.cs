using Employee_management.data;
using Employee_management.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Employee_management.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly EmployeeDbContext _context;

        public LeaveBalanceRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeLeaveBalance> GetLeaveBalanceAsync(int employeeId)
        {
            return await _context.EmployeeLeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeID == employeeId);
        }

        public async Task<bool> ResetLeaveBalanceAsync(int employeeId)
        {
            var leaveBalance = await _context.EmployeeLeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeID == employeeId);

            if (leaveBalance == null) return false;

            leaveBalance.UsedPaidLeaves = 0;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLeaveBalanceAsync(EmployeeLeaveBalance leaveBalance)
        {
            _context.EmployeeLeaveBalances.Update(leaveBalance);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
