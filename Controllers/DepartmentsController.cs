using Hw_3_CRUD.Data;
using Hw_3_CRUD.DTOs.Departments;
using Hw_3_CRUD.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hw_3_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = _context.Departments.ToList();

            var responseDepartmentsDto = departments.Adapt<IEnumerable<GetDepartemntsDto>>();

            return Ok(responseDepartmentsDto);
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == id);


            if (department == null)
            {
                return NotFound( "Department not found.");
            }

            var responseDepartmentsDto = department.Adapt<GetDepartemntsDto>();

            return Ok(responseDepartmentsDto);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest( "Invalid department data, Please Provide data !");
            }
            var department = departmentDto.Adapt<Department>();
            _context.Departments.Add(department);
            _context.SaveChanges(); 

            return Ok(departmentDto);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, UpdateDepartmentDto updateDepartmentDto)
        {

            var departmentToUpdate = _context.Departments.Find(id);

            if (departmentToUpdate == null)
            {
                return NotFound( "Department not found." );
            }

            updateDepartmentDto.Adapt(departmentToUpdate);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department not found." );
            }
             
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return Ok("Department deleted successfully.");
        }
    }
}
