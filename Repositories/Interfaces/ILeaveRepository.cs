using Employee_management.Models;

namespace Employee_management.Repositories
{
    public interface ILeaveRepository
    {

        Task<List<Leave>> GetAllLeaveAsync();
        Task<Leave> GetLeaveByIdAsync(int id);
        Task<Leave> ApplyLeaveAsync(Leave leave);
        Task<bool> UpdateLeaveAsync(int id, Leave leave);
        Task<bool> UpdateLeaveStatusAsync(int id, UpdateLeaveStatusDto request);
        Task<bool> DeleteLeaveAsync(int id);
    }
}