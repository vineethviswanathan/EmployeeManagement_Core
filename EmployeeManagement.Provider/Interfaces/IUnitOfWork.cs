using EmployeeManagement.Data.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Interfaces
{
    /// <summary>
    /// Interface for Unit of Work.
    /// </summary>
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> EmployeeRepository { get; }
        
        IGenericRepository<Department> DepartmentRepository { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
