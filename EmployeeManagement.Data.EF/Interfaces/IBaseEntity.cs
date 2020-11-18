using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Data.EF.Models
{
    public interface IBaseEntity
    {
        public int ID { get; set; }
    }
}
