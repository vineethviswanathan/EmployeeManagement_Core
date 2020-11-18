using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Common.Models
{
    public class DepartmentDto
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public virtual ICollection<EmployeeDto> Employees { get; set; }
    }
}
