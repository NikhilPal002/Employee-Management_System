using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management.Models
{
    public class Payroll
{
    [Key]
    public int PayrollID { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeID { get; set; }
    public virtual Employee Employee { get; set; }

    [Required, StringLength(10)]
    public string MonthYear { get; set; } // Format: YYYY-MM

    [Column(TypeName = "decimal(10,2)")]
    public decimal BasicSalary { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Deductions { get; set; } = 0.00M;

    [Column(TypeName = "decimal(10,2)")]
    public decimal NetSalary { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.Now;
}
}