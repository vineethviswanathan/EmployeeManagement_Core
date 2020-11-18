using EmployeeManagement.Data.EF;
using EmployeeManagement.Data.EF.Models;
using EmployeeManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeContext employeeContext;

        public UnitOfWork(EmployeeContext employeeContext, IGenericRepository<Employee> employeeRepository, IGenericRepository<Department> departmentRepository)
        {
            this.employeeContext = employeeContext;
            this.EmployeeRepository = employeeRepository;
            this.DepartmentRepository = departmentRepository;


        }


        public IGenericRepository<Employee> EmployeeRepository { get; }


        public IGenericRepository<Department> DepartmentRepository { get; }

        public virtual Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return employeeContext.SaveChangesAsync(cancellationToken);
        }
    }
}
