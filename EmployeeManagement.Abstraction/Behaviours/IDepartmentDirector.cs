using EmployeeManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Abstraction.Behaviours
{
    public interface IDepartmentDirector
    {
        /// <summary>
        /// Get Departments.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<DepartmentDto>> GetAllDepartmentAsync(CancellationToken cancellationToken);
    }
}
