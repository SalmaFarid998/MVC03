using Company.Data.Contexts;
using Company.Data.Models;
using Company.Service.Mapping;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.Service.Interfaces;
using Company.Service;
using Company.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(x=>x.AddProfile(new DepartmentProfile()));
            //builder.Services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
            //builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.User.RequireUniqueEmail = true;
                config.Lockout.AllowedForNewUsers = true;
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<CompanyDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.LogoutPath = "/Account/SignOut";
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath= "/Account/AccessDenied";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=SignUp}/{id?}");

            app.Run();
        }
    }
}
