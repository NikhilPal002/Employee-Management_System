using Employee_management.Models;

namespace Employee_management.Repositories
{
    public interface IPayrollRepository
    {
        Task<List<Payroll>> GetAllPayrollsAsync();
        Task<List<Payroll>> GetPayrollsByEmployeeIdAsync(int employeeId);
        Task<Payroll> GeneratePayrollAsync(GeneratePayrollDto payroll);
        Task<Payroll> GetPayrollByIdAsync(int id);
        Task<bool> DeletePayrollAsync(int id);
    }

}