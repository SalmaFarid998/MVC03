﻿using Company.Data.Models;
using Company.Repository.Interfaces.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.DTO.Employee
{
    public class EmployeeDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public string ImageURL { get; set; }
        public DepartmentDto? Department { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
