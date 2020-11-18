using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Abstraction.Behaviours;
using EmployeeManagement.Common.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeDirector employeeDirector;
        public EmployeeController(IEmployeeDirector employeeDirector)
        {
            this.employeeDirector = employeeDirector;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> Get()
        
        {
            return await employeeDirector.GetEmployeesAsync(default).ConfigureAwait(false);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            return await employeeDirector.GetEmployeeByIdAsync(id, default).ConfigureAwait(false);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeDto employee)
        {
            return await employeeDirector.AddEmployeeAsync(employee, default).ConfigureAwait(false);
        }

        // PUT api/<EmployeeController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<EmployeeDto>> Put(int id, [FromBody] EmployeeDto employee)
        {
            return await employeeDirector.UpdateEmployeeAsync(id,employee, default).ConfigureAwait(false);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await employeeDirector.DeleteEmployeeAsync(id, default).ConfigureAwait(false);
        }
    }
}
