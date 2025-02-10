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

        public async Task UpdateLeaveBalanceAsync(int employeeId, int usedLeaves)
        {
            var leaveBalance = await _context.EmployeeLeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeID == employeeId);

            if (leaveBalance != null)
            {
                leaveBalance.UsedPaidLeaves += usedLeaves;
                await _context.SaveChangesAsync();
            }
        }
    }
}
