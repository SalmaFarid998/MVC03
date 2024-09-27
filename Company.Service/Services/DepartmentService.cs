using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IDepartmentRepository DepartmentRepository { get; }

        public void Add(Department entity)
        {
            _departmentRepository.Add(entity);
        }

        public void Delete(Department entity)
        {
            _departmentRepository.Delete(entity);
        }

        public void Update(Department entity)
        {
            var dept = GetById(entity.Id);
            if (dept.Name != entity.Name)
            {
                if (GetAll().Any(x => x.Name == entity.Name))
                {
                    throw new Exception("Duplicate Department Name");
                }
                else
                {
                    dept.Name = entity.Name;
                    dept.Code = entity.Code;
                    _departmentRepository.Update(dept);
                }
            }
        }

        public IEnumerable<Department> GetAll()
        {
            var dept = _departmentRepository.GetAll().Where(x => x.IsDeleted != true);
            return dept;
        }

        public Department GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            else
            {
                var dept = _departmentRepository.GetById(id.Value);
                if (dept is null)
                {
                    return null;
                }
                else
                {
                    return dept;
                }
            }
        }
            
    }
}
