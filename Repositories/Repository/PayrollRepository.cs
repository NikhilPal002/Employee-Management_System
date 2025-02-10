using Employee_management.data;
using Employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.Repositories
{

    public class PayrollRepository : IPayrollRepository
    {
        private readonly EmployeeDbContext _context;

        public PayrollRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePayrollAsync(int id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            if (payroll == null) return false;

            _context.Payrolls.Remove(payroll);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Payroll> GeneratePayrollAsync(GeneratePayrollDto request)
        {
            var employee = await _context.Employees.FindAsync(request.EmployeeID);
            if (employee == null) return null;

            //  Fetch Leave Records (Only Approved Leaves)
            var leaveRecords = await _context.Leaves
            .Where(l => l.EmployeeID == request.EmployeeID && l.Status == "Approved")
            .ToListAsync();

            int totalLeaveDays = leaveRecords.Sum(l => (l.EndDate - l.StartDate).Days + 1);

            // Get Employee Leave Balance
            var leaveBalance = await _context.EmployeeLeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeID == request.EmployeeID);

            int availablePaidLeaves = leaveBalance?.PendingPaidLeaves ?? 22;
            int unpaidLeaveDays = Math.Max(totalLeaveDays - availablePaidLeaves, 0);



            int totalWorkingDays = 22; // Adjust based on company policy
            decimal perDaySalary = employee.Salary / totalWorkingDays;
            decimal unpaidLeaveDeduction = unpaidLeaveDays * perDaySalary;

            decimal deductions = request.Deductions + unpaidLeaveDeduction;
            decimal netSalary = employee.Salary - deductions;

            var payroll = new Payroll
            {
                EmployeeID = request.EmployeeID,
                MonthYear = request.MonthYear,
                BasicSalary = employee.Salary,
                Deductions = deductions,
                NetSalary = netSalary,
                PaymentDate = DateTime.Now
            };

            _context.Payrolls.Add(payroll);
            await _context.SaveChangesAsync();
            return payroll;
        }

        public async Task<List<Payroll>> GetAllPayrollsAsync()
        {
            return await _context.Payrolls.Include(p => p.Employee).ToListAsync();
        }

        public async Task<Payroll> GetPayrollByIdAsync(int id)
        {
            return await _context.Payrolls.FindAsync(id);
        }

        public async Task<List<Payroll>> GetPayrollsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Payrolls.Where(p => p.EmployeeID == employeeId).ToListAsync();
        }
    }
}