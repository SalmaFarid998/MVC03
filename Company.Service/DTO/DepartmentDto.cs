using Company.Repository.Interfaces.Employee.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.DTO.Department
{
    public class DepartmentDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
