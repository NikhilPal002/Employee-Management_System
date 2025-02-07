using System.ComponentModel.DataAnnotations;

namespace Employee_management.Models
{
    public class Role
{
    [Key]
    public int RoleID { get; set; }

    [Required, StringLength(50)]
    public string RoleName { get; set; }
}
}