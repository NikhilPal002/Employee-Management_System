using Employee_management.data;
using Employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            // Check if email already exists
            bool emailExists = await _context.Employees
                    .Include("Role")
                    .Include("Department").AnyAsync(e => e.Email == employee.Email);
            if (emailExists)
                return null;

            // Set default values
            employee.PasswordHash = "Default@123";
            employee.JoinDate = DateTime.UtcNow;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

             // 🔹 Add Leave Balance Entry
            var leaveBalance = new EmployeeLeaveBalance
            {
                EmployeeID = employee.EmployeeID,
                TotalPaidLeaves = 22,
                UsedPaidLeaves = 0
            };

            _context.EmployeeLeaveBalances.Add(leaveBalance);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var existingEmployee = await _context.Employees
                   .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (existingEmployee == null)
            {
                return null;
            }

            // 🔹 Remove Employee Leaves
            var employeeLeaves = _context.Leaves.Where(l => l.EmployeeID == id);
            _context.Leaves.RemoveRange(employeeLeaves);

            // 🔹 Remove Employee Payroll Records
            var payrollRecords = _context.Payrolls.Where(p => p.EmployeeID == id);
            _context.Payrolls.RemoveRange(payrollRecords);

            // 🔹 Remove Employee Leave Balance
            var leaveBalance = _context.EmployeeLeaveBalances.FirstOrDefault(lb => lb.EmployeeID == id);
            if (leaveBalance != null)
            {
                _context.EmployeeLeaveBalances.Remove(leaveBalance);
            }

            _context.Employees.Remove(existingEmployee);
            await _context.SaveChangesAsync();

            return existingEmployee;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                    .Include(e => e.Role)
                    .Include(e => e.Department)
                    .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                   .Include(e => e.Role)
                   .Include(e => e.Department)
                   .FirstOrDefaultAsync(e => e.EmployeeID == id);
        }

        public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
        {
            var existingEmployee = await _context.Employees
                   .Include("Role")
                    .Include("Department")
                   .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.RoleID = employee.RoleID;
            existingEmployee.DepartmentID = employee.DepartmentID;
            existingEmployee.Designation = employee.Designation;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.ProfilePicture = employee.ProfilePicture;
            existingEmployee.UpdatedAt = System.DateTime.Now;

            await _context.SaveChangesAsync();
            return existingEmployee;
        }
    }
}