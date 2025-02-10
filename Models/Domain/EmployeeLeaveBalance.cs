using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management.Models
{
    public class EmployeeLeaveBalance
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public int TotalPaidLeaves { get; set; } = 22;  // Annual limit
        public int UsedPaidLeaves { get; set; } = 0;
        public int PendingPaidLeaves => TotalPaidLeaves - UsedPaidLeaves; // Auto-calculated

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }

}