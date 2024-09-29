using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.Department;
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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        public IDepartmentRepository DepartmentRepository { get; }

        public void Add(DepartmentDto entity)
        {
            var MappedDepartment = new Department
            {
                Code = entity.Code,
                Name = entity.Name,
                CreatedAt = DateTime.Now,
            };

            _unitOfWork.departmentRepository.Add(MappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto entity)
        {
            Department dept = new Department
            {
                Name = entity.Name,
                Code = entity.Code,
                CreatedAt = DateTime.Now,
                
            };
            _unitOfWork.departmentRepository.Delete(dept);
            _unitOfWork.Complete();
        }

        public void Update(DepartmentDto entity)
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
                    _unitOfWork.departmentRepository.Update(dept);
                    _unitOfWork.Complete();
                }
            }
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var dept = _unitOfWork.departmentRepository.GetAll()/*.Where(x => x.IsDeleted != true)*/;

            var MappedDepartment = dept.Select(x => new DepartmentDto
            {
                Code = x.Code,
                Name = x.Name,
                Id = x.Id,
            });
            return MappedDepartment;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            else
            {
                var dept = _unitOfWork.departmentRepository.GetById(id.Value);
                if (dept is null)
                {
                    return null;
                }
                else
                {
                    DepartmentDto DeptDto = new DepartmentDto
                    {
                        Code = dept.Code,
                        Name = dept.Name,

                    };
                    return dept;
                }
            }
        }
            
    }
}
