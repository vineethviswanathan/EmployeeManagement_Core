using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Data.EF.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime? CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        string CreateBy { get; set; }
        string UpdateBy { get; set; }
    }
}
