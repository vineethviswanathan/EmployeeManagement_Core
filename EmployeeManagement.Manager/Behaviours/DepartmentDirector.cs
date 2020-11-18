using AutoMapper;
using EmployeeManagement.Abstraction.Behaviours;
using EmployeeManagement.Common.Models;
using EmployeeManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Behaviours
{
    public class DepartmentDirector: IDepartmentDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentRecommendationDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public DepartmentDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentAsync(CancellationToken cancellationToken)
        {
            var departments = await unitOfWork.DepartmentRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return departments.Select(x => mapper.Map<DepartmentDto>(x)).ToList();
        }
    }
}
