using Employee_management.data;
using Employee_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly EmployeeDbContext _context;

        public LeaveRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Leave> ApplyLeaveAsync(Leave leave)
        {
            if (leave.StartDate >= leave.EndDate)
            {
                throw new ArgumentException("Start Date must be before End Date.");
            }

            leave.Status = "Pending";
            leave.AppliedDate = DateTime.Now;

            _context.Leaves.Add(leave);
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<Leave> ApproveLeaveAsync(int id)
        {
            var leave = await _context.Leaves.FirstOrDefaultAsync(l => l.LeaveID == id);
            if (leave == null)
            {
                throw new Exception("The leave ID not found");
            }

            int leaveDays = (leave.EndDate - leave.StartDate).Days + 1;

            var leaveBalance = await _context.EmployeeLeaveBalances
        .FirstOrDefaultAsync(lb => lb.EmployeeID == leave.EmployeeID);

            if (leaveBalance != null)
            {
                leaveBalance.UsedPaidLeaves += leaveDays; // Update the used leaves
                await _context.SaveChangesAsync();
            }

            leave.Status = "Approved";
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<bool> DeleteLeaveAsync(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null || leave.Status != "Pending")
                return false;

            _context.Leaves.Remove(leave);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Leave>> GetAllLeaveAsync()
        {
            return await _context.Leaves
                              .Include(l => l.Employee)
                              .Include(l => l.ApprovedByEmployee)
                              .ToListAsync();

        }

        public async Task<Leave> GetLeaveByIdAsync(int id)
        {
            return await _context.Leaves
                              .Include(l => l.Employee)
                              .Include(l => l.ApprovedByEmployee)
                              .FirstOrDefaultAsync(l => l.LeaveID == id);
        }

        public async Task<bool> UpdateLeaveAsync(int id, Leave leave)
        {
            var existingLeave = await _context.Leaves.FindAsync(id);
            if (existingLeave == null || existingLeave.Status != "Pending")
                return false;

            existingLeave.LeaveType = leave.LeaveType;
            existingLeave.StartDate = leave.StartDate;
            existingLeave.EndDate = leave.EndDate;

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UpdateLeaveStatusAsync(int id, UpdateLeaveStatusDto request)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null || leave.Status != "Pending")
                return false;

            if (request.Status == "Rejected")
            {
                leave.Status = "Rejected";
                await _context.SaveChangesAsync();
                return true;
            }

            if (request.Status == "Approved")
            {
                var leaveBalance = await _context.EmployeeLeaveBalances
                    .FirstOrDefaultAsync(lb => lb.EmployeeID == leave.EmployeeID);

                if (leaveBalance != null)
                {
                    int leaveDays = (leave.EndDate - leave.StartDate).Days + 1;
                    leaveBalance.UsedPaidLeaves += leaveDays;
                    await _context.SaveChangesAsync();
                }
            }

            leave.Status = request.Status;
            leave.ApprovedBy = request.ApprovedBy;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}