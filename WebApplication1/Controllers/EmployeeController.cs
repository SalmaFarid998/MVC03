using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    [Authorize(Roles ="HR")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var emp = _employeeService.GetAll();
                ViewBag.Message = "This is message";
                return View(emp);
            }
            else
            {
                var emp = _employeeService.GetEmployeeByName(searchInp);
                return View(emp);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _employeeService.Add(employee);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Employee", "Validation Error");
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EmployeeError", ex.Message);
                return View(employee);
            }

        }
    }
}
