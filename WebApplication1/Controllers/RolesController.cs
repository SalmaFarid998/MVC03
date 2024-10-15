using Company.Data.Models;
using Company.Web.Models;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    public class RolesController : Controller
    {

        public RoleManager<IdentityRole> _RoleManager { get; }
        public UserManager<ApplicationUser> _UserManager { get; }
        public ILogger<RolesController> _Logger { get; }

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<RolesController> logger)
        {
            _RoleManager = roleManager;
            _UserManager = userManager;
            _Logger = logger;
        }

        
        public async Task<IActionResult> Index()
        {
            var roles = await _RoleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {

        return View(); 
    }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = roleViewModel.Name,
                };
                var res = await _RoleManager.CreateAsync(role);
                if (res.Succeeded) { 
                    return RedirectToAction("Index");
                }
                foreach (var i in res.Errors)
                {
                    _Logger.LogInformation(i.Description);
                }

            }return View(roleViewModel);
        }


        public async Task<IActionResult> Details(string? id, string viewname = "Details")
        {
            var role = await _RoleManager.FindByIdAsync(id);
            if (role is null)
            {
                return NotFound();
            }
            var roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(viewname,roleViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id,"Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string? id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _RoleManager.FindByIdAsync(id);
                    if (role is null)
                    {
                        return NotFound();
                    }
                    role.Name = roleViewModel.Name;
                    role.NormalizedName = roleViewModel.Name.ToUpper();

                    var res = await _RoleManager.UpdateAsync(role);
                    if (res.Succeeded)
                    {
                        _Logger.LogInformation("user Updated Successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _Logger.LogInformation("User Update failed");
                        Console.WriteLine("Error In Update");
                        return View(roleViewModel);
                    }
                }
                catch (Exception ex)
                {
                    _Logger.LogInformation(ex.Message);
                }
                
            }
            return View(roleViewModel);

        }



        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _RoleManager.FindByIdAsync(id);
                if (role is null)
                {

                    return NotFound();
                }
                var res = await _RoleManager.DeleteAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                //else
                foreach (var i in res.Errors)
                {
                    _Logger.LogError(i.Description);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }
            return await Delete(id);
        }
    }
        

}
