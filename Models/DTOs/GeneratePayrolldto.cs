namespace Employee_management.Models
{
    public class GeneratePayrollDto
    {
        public int EmployeeID { get; set; }
        public string MonthYear { get; set; }
        public decimal Deductions { get; set; }
    }
}