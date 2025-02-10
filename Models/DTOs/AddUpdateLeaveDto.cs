using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management.Models
{
    public class AddLeaveDto
    {
        public int EmployeeID { get; set; }

        [Required, StringLength(50)]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }

    public class UpdateLeaveStatusDto
    {
        public string Status { get; set; } // "Approved" or "Rejected"
        public int ApprovedBy { get; set; } // Admin/HR ID
    }


    public class UpdateLeaveDto
    {

        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }

        [Required, StringLength(50)]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}