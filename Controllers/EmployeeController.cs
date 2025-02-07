using AutoMapper;
using Employee_management.data;
using Employee_management.Models;
using Employee_management.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_management.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeRepository.GetAllEmployeesAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound(new { message = "No employees found." });
            }

            var employeeDto = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(employeeDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employees = await employeeRepository.GetEmployeeByIdAsync(id);

            if (employees == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employees);
            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] AddUpdateEmployeeDto addEmployeeDto)
        {
            // Map DTO to Employee Entity
            var employee = _mapper.Map<Employee>(addEmployeeDto);

            // Add employee to repository
            var createdEmployee = await employeeRepository.AddEmployeeAsync(employee);

            if (createdEmployee == null)
                return BadRequest(new { message = "Email already exists" });

            // Map to DTO for response
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeID }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] AddUpdateEmployeeDto updateEmployeeDto)
        {
            // Map DTO to Employee Entity
            var employee = _mapper.Map<Employee>(updateEmployeeDto);

            // Add employee to repository
            employee = await employeeRepository.UpdateEmployeeAsync(id, employee);
            if (employee == null)
            {
                return NotFound();
            }

            // Map to DTO for response
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(new { message = "Employee Details Updated successfully", employeeDto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            var employees = await employeeRepository.DeleteEmployeeAsync(id);

            if (employees == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employees);
            return Ok(new { message = "Employee Deleted Successfully" });
        }


    }
}