using System.ComponentModel.DataAnnotations;

namespace Employee_management.Models
{
    public class Department
{
    [Key]
    public int DepartmentID { get; set; }

    [Required, StringLength(100)]
    public string DepartmentName { get; set; }
}
}