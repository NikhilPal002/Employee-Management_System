namespace Employee_management.Models
{
    public class PayrollDto
    {
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public string MonthYear { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}