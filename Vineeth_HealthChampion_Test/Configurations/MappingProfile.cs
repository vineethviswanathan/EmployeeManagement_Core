using AutoMapper;
using EmployeeManagement.Common.Models;
using EmployeeManagement.Data.EF.Models;

namespace EmployeeManagement.Service.Configurations
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Department,DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}