using Company.Data.Models;
using Company.Repository.Interfaces.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentDto GetById(int? id);
        IEnumerable<DepartmentDto> GetAll();
        void Add(DepartmentDto employee);
        void Update(DepartmentDto employee);
        void Delete(DepartmentDto employee);
    }
}
