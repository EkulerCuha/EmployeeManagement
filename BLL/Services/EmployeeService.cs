using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class EmployeeService : ServiceBase, IService<Employee, EmployeeModel>
{
    public EmployeeService(Db db) : base(db) { }

    public IQueryable<EmployeeModel> Query()
    {
        return _db.Employees.Include(e => e.Department).Include(p => p.EmployeeProjects).ThenInclude(ep => ep.Project).OrderBy(e => e.BirthDate).ThenByDescending(e => e.HireDate).ThenBy(e => e.IsFemale).ThenBy(e => e.FirstName).ThenBy(e => e.LastName).Select(e => new EmployeeModel() { Record = e });
    }

    public ServiceBase Create(Employee record)
    {
        if( _db.Employees.Any(e => e.FirstName == record.FirstName && e.LastName == record.LastName && e.Ssn == record.Ssn))
            return Error("Same employee already exists");
        record.FirstName = record.FirstName.Trim();
        record.LastName = record.LastName.Trim();
        _db.Employees.Add(record);
        _db.SaveChanges();
        return Success("Employee created");
    }

    public ServiceBase Update(Employee record)
    {
        if(_db.Employees.Any(e => e.Id != record.Id && e.FirstName == record.FirstName && e.LastName == record.LastName && e.Ssn == record.Ssn))
            return Error("Same employee already exists");
        
        var entity = _db.Employees.Include(e => e.EmployeeProjects).SingleOrDefault(e => e.Id == record.Id);
        if(entity is null)
            return Error("Employee not found");
        
        _db.EmployeeProjects.RemoveRange(entity.EmployeeProjects);
        
        entity.FirstName = record.FirstName.Trim();
        entity.LastName = record.LastName.Trim();
        entity.Ssn = record.Ssn;
        entity.IsFemale = record.IsFemale;
        entity.BirthDate = record.BirthDate;
        entity.HireDate = record.HireDate;
        entity.Salary = record.Salary;
        entity.DepartmentId = record.DepartmentId;

        entity.EmployeeProjects = entity.EmployeeProjects;
        
        _db.Employees.Update(entity);
        _db.SaveChanges();
        return Success("Employee updated");
    }

    public ServiceBase Delete(int id)
    {
        var employee = _db.Employees.Include(e => e.Department).SingleOrDefault(e => e.Id == id);
        if(employee is null)
            return Error("Employee not found");
        
        _db.EmployeeProjects.RemoveRange(employee.EmployeeProjects);
        
        _db.Employees.Remove(employee);
        _db.SaveChanges();
        return Success("Employee deleted");
    }
    
}