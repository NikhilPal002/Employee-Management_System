using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management.Models
{
    public class LeaveDto
    {
        public int LeaveID { get; set; }

        public int EmployeeID { get; set; }
        public EmployeeDto Employee { get; set; }

        [Required, StringLength(50)]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; }

        public DateTime AppliedDate { get; set; }

        public int? ApprovedBy { get; set; }

        public EmployeeDto ApprovedByEmployee { get; set; }
    }

}