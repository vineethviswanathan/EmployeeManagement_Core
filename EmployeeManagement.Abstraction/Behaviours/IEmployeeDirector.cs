using EmployeeManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Abstraction.Behaviours
{
    public interface IEmployeeDirector
    {
        /// <summary>
        /// Get Employees.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<EmployeeDto>> GetEmployeesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get AssessmentResult by Id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<EmployeeDto> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Update Employee.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="employeeDto">Update employee.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task<EmployeeDto> UpdateEmployeeAsync(
            int id, EmployeeDto employeeDto, CancellationToken cancellationToken);

        /// <summary>
        /// Add Employee.
        /// </summary>
        /// <param name="employeeDto">add Employee.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<EmployeeDto> AddEmployeeAsync(
             EmployeeDto employeeDto, CancellationToken cancellationToken);

        /// <summary>
        /// Delete Employee.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken);
    }
}
