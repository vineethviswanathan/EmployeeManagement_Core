using AutoMapper;
using EmployeeManagement.Abstraction.Behaviours;
using EmployeeManagement.Common.Models;
using EmployeeManagement.Data.EF.Models;
using EmployeeManagement.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Behaviours
{
    public class EmployeeDirector : IEmployeeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentRecommendationDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public EmployeeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            await this.unitOfWork.EmployeeRepository.InsertAsync(employee, cancellationToken).ConfigureAwait(false);
            await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            employeeDto.ID = employee.ID;
            return employeeDto;
        }

        public async Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken)
        {
            await unitOfWork.EmployeeRepository.DeleteAsync(x => x.ID == id, cancellationToken).ConfigureAwait(false);

            return await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false) > 0;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken)
        {
            
            var Employee = await unitOfWork.EmployeeRepository.FindByAsync(x => x.ID == id, cancellationToken).ConfigureAwait(false);
            if (Employee != null)
            {
                var EmployeeContext = unitOfWork.EmployeeRepository.GetContext();
                Employee.Reportees = GetEmployeesAllLevels(EmployeeContext, Employee.ID);
            }

            return mapper.Map<EmployeeDto>(Employee);
        }

        private static List<Employee> GetEmployeesAllLevels(DbSet<Employee> employees, int managerId)
        {
            var existingList = new List<Employee>();
            var lstSelectedEmployees1 = employees.Where(emp => emp.ManagerID == managerId).AsNoTracking()
                                                .ToList();
            foreach (var emp in lstSelectedEmployees1)
            {
                emp.Reportees = GetEmployeesAllLevels(employees, emp.ID);
                existingList.Add(emp);
            }
            return existingList;
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            var employees = (await unitOfWork.EmployeeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false));
            foreach (Employee item in employees)
            {
                item.Manager = await unitOfWork.EmployeeRepository.FindByAsync(x => x.ID == item.ManagerID, cancellationToken).ConfigureAwait(false);
                item.Department = await unitOfWork.DepartmentRepository.FindByAsync(x => x.ID == item.DepartmentID, cancellationToken).ConfigureAwait(false);
            }
            return employees.Select(x => mapper.Map<EmployeeDto>(x)).ToList();
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork.EmployeeRepository.FindByAsync(x => x.ID == id, cancellationToken).ConfigureAwait(false);
            employee.ManagerID = employeeDto.ManagerID;
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Address = employeeDto.Address;
            employee.DOJ = employeeDto.DOJ;
            employee.DepartmentID = employeeDto.DepartmentID;

            await this.unitOfWork.EmployeeRepository.UpdateAsync(employee, cancellationToken).ConfigureAwait(false);
            await unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
            employeeDto.ID = employee.ID;
            return employeeDto;
        }
    }
}
