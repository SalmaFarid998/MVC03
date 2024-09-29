﻿using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Repository.Interfaces.Employee.DTO;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
                private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(EmployeeDto employeeDto)
        {
            //Employee employee = new Employee
            //{
            //    Address = employeeDto.Address,
            //    Age = employeeDto.Age,
            //    DepartmentId = employeeDto.DepartmentId,
            //    Email = employeeDto.Email,
            //    HiringDate = employeeDto.HiringDate,
            //    ImageURL = employeeDto.ImageURL,
            //    Name = employeeDto.Name,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary,
            //};
            Employee employee = _mapper.Map<Employee>(employeeDto);

            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {
            //Employee employee = new Employee
            //{
            //    Address = employeeDto.Address,
            //    Age = employeeDto.Age,
            //    DepartmentId = employeeDto.DepartmentId,
            //    Email = employeeDto.Email,
            //    HiringDate = employeeDto.HiringDate,
            //    ImageURL = employeeDto.ImageURL,
            //    Name = employeeDto.Name,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary,
            //};

            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.employeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
           var emp = _unitOfWork.employeeRepository.GetAll();

            //var MappedEmployee = emp.Select(emp => new EmployeeDto
            //{
            //    DepartmentId = emp.DepartmentId,
            //    Address = emp.Address,
            //    Salary = emp.Salary,
            //    HiringDate = emp.HiringDate,
            //    ImageURL = emp.ImageURL,
            //    Name = emp.Name,
            //    PhoneNumber = emp.PhoneNumber,
            //    CreatedAt = emp.CreatedAt
            //});
            IEnumerable<EmployeeDto> MappedEmployee = _mapper.Map<IEnumerable<EmployeeDto>>(emp);
            return MappedEmployee;
        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            else
            {
                var emp = _unitOfWork.employeeRepository.GetById(id.Value);
                if (emp is null)
                {
                    return null;
                }
                else
                {
                    //EmployeeDto employeeDto = new EmployeeDto
                    //{
                    //    Address = emp.Address,
                    //    Age = emp.Age,
                    //    DepartmentId = emp.DepartmentId,
                    //    Email = emp.Email,
                    //    HiringDate = emp.HiringDate,
                    //    ImageURL = emp.ImageURL,
                    //    Name = emp.Name,
                    //    PhoneNumber = emp.PhoneNumber,
                    //    Salary = emp.Salary,
                    //};
                   EmployeeDto  employeeDto = _mapper.Map<EmployeeDto>(emp);
                    return employeeDto;
                }
            }
        }

        public EmployeeDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name) {
            var emp = _unitOfWork.employeeRepository.GetEmployeeByName(name);
            //IEnumerable<EmployeeDto> employeeDto = new EmployeeDto
            //{
            //    Address = emp.Address,
            //    Age = emp.Age,
            //    DepartmentId = emp.DepartmentId,
            //    Email = emp.Email,
            //    HiringDate = emp.HiringDate,
            //    ImageURL = emp.ImageURL,
            //    Name = emp.Name,
            //    PhoneNumber = emp.PhoneNumber,
            //    Salary = emp.Salary,
            //};

            IEnumerable<EmployeeDto> MappedEmployee = _mapper.Map<IEnumerable<EmployeeDto>>(emp);
            return MappedEmployee;

        }

        public void Update(EmployeeDto employee)
        {

            _unitOfWork.employeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
