using Hw_3_CRUD.Data;
using Hw_3_CRUD.DTOs.Employees;
using Hw_3_CRUD.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hw_3_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employees = _context.Employees.ToList();

            var employeesDto = employees.Adapt<IEnumerable<GetEmployeeDto>>();

            return Ok(employeesDto);
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var employeeDto = employee.Adapt<GetEmployeeDto>(); 

            return Ok(employeeDto);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Invalid employee data.");
            }

            var employee = employeeDto.Adapt<Employee>();
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employeeToUpdate = _context.Employees.Find(id);
            if (employeeToUpdate == null)
            {
                return NotFound("Employee not found.");
            }

            updateEmployeeDto.Adapt(employeeToUpdate);

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
