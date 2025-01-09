using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public class Db : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public Db(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
    }
}