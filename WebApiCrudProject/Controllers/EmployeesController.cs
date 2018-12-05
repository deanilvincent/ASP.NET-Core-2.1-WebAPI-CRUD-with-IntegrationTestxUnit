using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCrudProject.Models;
using WebApiCrudProject.Repositories;

namespace WebApiCrudProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepo;

        public EmployeesController(IEmployeeRepository employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await employeeRepo.Employees());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
                return BadRequest();

            if (!await employeeRepo.EmployeeIdExists(id))
                return BadRequest();

            return Ok(await employeeRepo.EmployeeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (!await employeeRepo.CreateEmployee(employee))
                return BadRequest();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Employee employee, int id)
        {
            await EmployeeValidation(id);

            employee.EmployeeId = id;
            if (!await employeeRepo.UpdateEmployee(employee))
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await EmployeeValidation(id);

            if (!await employeeRepo.DeleteEmployeeById(id))
                return BadRequest();

            return Ok();
        }

        private async Task<IActionResult> EmployeeValidation(int id)
        {
            if (id == 0)
                return BadRequest();

            if (!await employeeRepo.EmployeeIdExists(id))
                return BadRequest();

            return Ok();
        }
    }
}
