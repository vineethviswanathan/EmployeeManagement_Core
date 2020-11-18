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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentDirector departmentDirector;
        public DepartmentController(IDepartmentDirector departmentDirector)
        {
            this.departmentDirector = departmentDirector;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> Get()

        {
            return await departmentDirector.GetAllDepartmentAsync(default).ConfigureAwait(false);
        }
    }
}
