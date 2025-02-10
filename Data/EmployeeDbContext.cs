using Employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "HR" },
                new Role { RoleID = 3, RoleName = "Employee" }
            );

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeLeaveBalance>()
                .HasOne(e => e.Employee)
                .WithOne() // Assuming one leave balance per employee
                .HasForeignKey<EmployeeLeaveBalance>(e => e.EmployeeID);
        }


    }
}
