using EmployeeManagement.Data.EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Data.EF.Models
{
    public class Department : IBaseEntity, IAuditableEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        
    }
}
