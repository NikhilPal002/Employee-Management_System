using AutoMapper;
using Employee_management.Models;
using Employee_management.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Employee_management.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IMapper _mapper;

        public PayrollController(IPayrollRepository payrollRepository, IMapper mapper)
        {
            _payrollRepository = payrollRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetPayrolls()
        {
            var payrolls = await _payrollRepository.GetAllPayrollsAsync();
            var payrollDtos = _mapper.Map<IEnumerable<PayrollDto>>(payrolls);
            return Ok(payrollDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPayrollById(int id)
        {
            var payrolls = await _payrollRepository.GetPayrollByIdAsync(id);
            if (payrolls == null)
                return NotFound(new { message = "No payroll records found" });

            var payrollDtos = _mapper.Map<PayrollDto>(payrolls);
            return Ok(payrollDtos);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult> GetEmployeePayroll(int employeeId)
        {
            var payrolls = await _payrollRepository.GetPayrollsByEmployeeIdAsync(employeeId);
            if (!payrolls.Any())
                return NotFound(new { message = "No payroll records found" });

            var payrollDtos = _mapper.Map<List<PayrollDto>>(payrolls);
            return Ok(payrollDtos);
        }

        [HttpPost]
        public async Task<ActionResult> GeneratePayroll(GeneratePayrollDto request)
        {
            var payroll = await _payrollRepository.GeneratePayrollAsync(request);
            if (payroll == null)
                return NotFound(new { message = "Employee not found" });

            return CreatedAtAction(nameof(GetEmployeePayroll), new { employeeId = payroll.EmployeeID }, _mapper.Map<PayrollDto>(payroll));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayroll(int id)
        {
            var result = await _payrollRepository.DeletePayrollAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }


    }
}