using AutoMapper;
using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
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
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IDepartmentRepository DepartmentRepository { get; }

        public void Add(DepartmentDto entity)
        {
            //var MappedDepartment = new Department
            //{
            //    Code = entity.Code,
            //    Name = entity.Name,
            //    CreatedAt = DateTime.Now,
            //};
            Department department = _mapper.Map<Department>(entity);

            _unitOfWork.departmentRepository.Add(department);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto entity)
        {
            //Department dept = new Department
            //{
            //    Name = entity.Name,
            //    Code = entity.Code,
            //    CreatedAt = DateTime.Now,
            //    Id = entity.Id
            //};
            Department dept = _mapper.Map<Department>(entity);
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
            var dept = _unitOfWork.departmentRepository.GetAll().Where(x => x.IsDeleted != true);

            //var MappedDepartment = dept.Select(x => new DepartmentDto
            //{
            //    Code = x.Code,
            //    Name = x.Name,
            //    Id = x.Id

            //});
            IEnumerable<DepartmentDto> MappedDept = _mapper.Map<IEnumerable<DepartmentDto>>(dept);

            return MappedDept;
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
                    //DepartmentDto MappedDepartment = new DepartmentDto
                    //{
                    //    Code= dept.Code,
                    //    Name= dept.Name,
                    //    Id = dept.Id
                    //};
                    DepartmentDto DeptDto = _mapper.Map<DepartmentDto>(dept);
                    return DeptDto;
                }
            }
        }
            
    }
}
