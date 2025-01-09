using System.ComponentModel;
using BLL.DAL;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using BLL.Services;
using BLL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IOC
// var connectionString = "Server=localhost;Database=employees;User Id=furkan;Password=123123;"; // Connection String for POSTGRESQL
string connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<Db>(options => options.UseNpgsql(connectionString)); // and I used UseNpsql instead of UseSqlServer
//builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString)); // for local db

builder.Services.AddScoped<IService<Department, DepartmentModel>, DepartmentService>();
builder.Services.AddScoped<IService<Employee, EmployeeModel>, EmployeeService>();
builder.Services.AddScoped<IService<User, UserModel>, UserService>();
builder.Services.AddScoped<IService<Project, ProjectModel>, ProjectService>();

//auth

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
        options.AccessDeniedPath = "/Users/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

//appsettigns

var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();