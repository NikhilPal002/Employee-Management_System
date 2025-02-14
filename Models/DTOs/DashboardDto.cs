namespace Employee_management.Models
{
    public class EmployeeOverviewDto
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int NewHiresThisMonth { get; set; }
    }

    public class DashboardStatisticsDto
    {
        public int TotalEmployees { get; set; }
        public int EmployeesPresentToday { get; set; }
        public int EmployeesOnLeaveToday { get; set; }
        public decimal TotalPayrollPaidThisMonth { get; set; }
        public int PendingLeaveRequests { get; set; }
    }

}