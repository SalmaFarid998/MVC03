using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dto;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var dept = _departmentService.GetAll();
            return View(dept);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dept = _mapper.Map<DepartmentDto>(department);
                    _departmentService.Add(dept);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Deparment", "Validation Error");
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeparmentError", ex.Message);
                return View(department);
            }

        }
        [HttpGet]
        public IActionResult Details(int? id,string viewname = "Details")
        {
            var dept = _departmentService.GetById(id);

            if (dept is null)
            {
                return NotFound();
            }
            else
            {
                return View(viewname, dept);
            }
        }
        [HttpGet]  
        public IActionResult Update(int? id)
        {
            return Details(id,"Update");
        }

        [HttpPost]
        public IActionResult Update(int? id,Department department)
        {
            if(department.Id != id.Value)
            {
                return RedirectToAction("NotFoundPage", null,"Home");
            }
            else
            {
                var dept = _mapper.Map<DepartmentDto>(department);
                _departmentService.Update(dept);
                return RedirectToAction(nameof(Index));
            }
        }
        //Hard Delete
        //[HttpPost]
        //public IActionResult Delete(int? id)
        //{
        //    var dept = _departmentService.GetById(id);
        //    if (dept is null) {
        //        return RedirectToAction("NotFoundPage", null, "Home");
        //    }
        //    else
        //    {
        //        _departmentService.Delete(dept);
        //        return RedirectToAction(nameof(Index));
        //    }

        //}

        //SoftDelete
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var dept = _departmentService.GetById(id);
            if (dept is null)
            {
                return RedirectToAction("NotFoundPage", null, "Home");
            }
            else
            {
                //dept.IsDeleted = true;
                _departmentService.Update(dept);
                return RedirectToAction(nameof(Index));
            }

        }



    }
}

