using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
using Company.Service.Helper;
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
        public void Add(EmployeeDto entityDto)
        {
            //Employee employee = new Employee
            //{
            //    //Manual Mapping
            //    Address = entityDto.Address,
            //    Age = entityDto.Age,
            //    DepartmentId = entityDto.DepartmentId,
            //    Email = entityDto.Email,
            //    HiringDate = entityDto.HiringDate,
            //    ImageURL = entityDto.ImageURL,
            //    Name = entityDto.Name,
            //    PhoneNumber = entityDto.PhoneNumber,
            //    Salary = entityDto.Salary
            //};
            Employee employee = _mapper.Map<Employee>(entityDto);
            employee.ImageURL = DocumentSettings.UploadFile(entityDto.Image,"Images");

            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto entityDto)
        {
            //Employee employee = new Employee
            //{
            //    //Manual Mapping
            //    Address = entityDto.Address,
            //    Age = entityDto.Age,
            //    DepartmentId = entityDto.DepartmentId,
            //    Email = entityDto.Email,
            //    HiringDate = entityDto.HiringDate,
            //    ImageURL = entityDto.ImageURL,
            //    Name = entityDto.Name,
            //    PhoneNumber = entityDto.PhoneNumber,
            //    Salary = entityDto.Salary
            //};
            Employee employee = _mapper.Map<Employee>(entityDto);
            _unitOfWork.employeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            

           var emp = _unitOfWork.employeeRepository.GetAll();

            //var MappedEmployee = emp.Select(x => new EmployeeDto
            //{
            //    DepartmentId = x.DepartmentId,
            //    Address = x.Address,
            //    Age = x.Age,
            //    Salary = x.Salary,
            //    HiringDate = x.HiringDate,
            //    ImageURL = x.ImageURL,
            //    Name = x.Name,
            //    PhoneNumber = x.PhoneNumber,
            //    CreatedAt = x.CreatedAt
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
                    //EmployeeDto MappedEmployee =  new EmployeeDto
                    //{
                    //    DepartmentId = emp.DepartmentId,
                    //    Address = emp.Address,
                    //    Age = emp.Age,
                    //    Salary = emp.Salary,
                    //    HiringDate = emp.HiringDate,
                    //    ImageURL = emp.ImageURL,
                    //    Name = emp.Name,
                    //    PhoneNumber = emp.PhoneNumber,
                    //    CreatedAt = emp.CreatedAt
                    //};

                    EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(emp);
                    return employeeDto;
                }
            }
        }

        public EmployeeDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
            IEnumerable<Employee> emp = _unitOfWork.employeeRepository.GetEmployeeByName(name);

            IEnumerable<EmployeeDto> MappedEmployee = _mapper.Map<IEnumerable<EmployeeDto>>(emp);
            return MappedEmployee;
        }

        public void Update(EmployeeDto employee)
        {
            var emp = _mapper.Map<Employee>(employee);
            _unitOfWork.employeeRepository.Update(emp);
            _unitOfWork.Complete();
        }
    }
}
