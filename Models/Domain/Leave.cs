using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management.Models
{
    public class Leave
{
    [Key]
    public int LeaveID { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeID { get; set; }
    public virtual Employee Employee { get; set; }

    [Required, StringLength(50)]
    public string LeaveType { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required, StringLength(50)]
    public string Status { get; set; } = "Pending"; // Default: Pending, Approved, Rejected

    public DateTime AppliedDate { get; set; } = DateTime.Now;

    [ForeignKey("ApprovedByEmployee")]
    public int? ApprovedBy { get; set; }
    public virtual Employee ApprovedByEmployee { get; set; }
}
}