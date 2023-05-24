using EmployeeService.Models;
using EmployeeService.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _services;
        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
        }
        [HttpGet]
        
        public async Task<ActionResult> GetEmployees()
        {
            var emp = await _services.GetAllEmployees();
            if(emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetEmployeeById(int id)
        {
            var emp = await _services.GetEmployeeById(id);
            if(emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            var emp = await _services.CreateEmployee(employee);
            if(emp == null)
            {
                return BadRequest();
            }
            return Ok(emp);

        }
        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var deleteEmp = _services.DeleteEmployee(id);
            if(deleteEmp== null)
            {
                return BadRequest();
            }
            return Ok(deleteEmp);
        }
        [HttpPost("filter/salary")]
        public async Task<ActionResult> GetEmployeesById(int salary)
        {
            var emp = await _services.GetEmployeeBySalary(salary);
            if(emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
    }
}
