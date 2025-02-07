using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management.Models
{
    public class Attendance
{
    [Key]
    public int AttendanceID { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeID { get; set; }
    public virtual Employee Employee { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    public TimeSpan? CheckInTime { get; set; }
    public TimeSpan? CheckOutTime { get; set; }
}
}