public class AttendanceSummaryDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public int PresentDays { get; set; }
    public int AbsentDays { get; set; }
    public int LeaveDays { get; set; }
    public double AttendancePercentage { get; set; }
}

public class LeaveSummaryDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public int TotalLeaves { get; set; }
    public int PaidLeaves { get; set; }
    public int UnpaidLeaves { get; set; }
    public int PendingPaidLeaves { get; set; }  // From EmployeeLeaveBalance
}


public class PayrollSummaryDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetSalary { get; set; }
}
