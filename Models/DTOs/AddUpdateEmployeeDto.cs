using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management.Models
{
    public class AddUpdateEmployeeDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentID { get; set; }

        [StringLength(100)]
        public string Designation { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }
        public string ProfilePicture { get; set; }
    }
}