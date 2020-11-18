using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Common.Models
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DOJ { get; set; }
        public int? ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public virtual EmployeeDto Manager { get; set; }
        public virtual DepartmentDto Department { get; set; }
        public virtual ICollection<EmployeeDto> Reportees { get; set; }
    }
}
