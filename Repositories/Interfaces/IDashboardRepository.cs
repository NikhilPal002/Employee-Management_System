using Employee_management.Models;

namespace Employee_management.Repositories
{
    public interface IDashboardRepository
    {
        Task<EmployeeOverviewDto> GetEmployeesOverview();
        Task<DashboardStatisticsDto> GetDashboardStatistics();
    }
}