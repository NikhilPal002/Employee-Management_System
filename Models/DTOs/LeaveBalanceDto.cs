namespace Employee_management.Models
{
    public class LeaveBalanceDto
    {
        public int EmployeeID { get; set; }
        public int TotalPaidLeaves { get; set; } = 22; // Default yearly paid leaves
        public int UsedPaidLeaves { get; set; }
        public int RemainingPaidLeaves => TotalPaidLeaves - UsedPaidLeaves; // Auto-calculated
    }

    
}
