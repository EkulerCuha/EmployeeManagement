using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class DepartmentService : ServiceBase, IService<Department, DepartmentModel>
{
    public DepartmentService(Db db) : base(db) { }

    public IQueryable<DepartmentModel> Query()
    {
        return _db.Departments.OrderBy(d => d.Name).Select(d => new DepartmentModel() { Record = d });
    }

    public ServiceBase Create(Department record)
    {
        if ( _db.Departments.Any(d => d.Name.ToUpper() == record.Name.ToUpper().Trim()))
            return Error("A department with the same name already exists.");
        
        record.Name = record.Name?.Trim();
        record.Description = record.Description?.Trim();
        _db.Departments.Add(record);
        _db.SaveChanges();
        return Success("Department is created successfully.");
    }

    public ServiceBase Update(Department record)
    {
        if ( _db.Departments.Any(d => d.Id != record.Id && d.Name.ToUpper() == record.Name.ToUpper().Trim()))
            return Error("A department with the same name already exists.");
        
        var entity = _db.Departments.SingleOrDefault(d => d.Id == record.Id);
        
        if(entity is null)
            return Error("Department with this id does not exist.");
        
        entity.Name = record.Name?.Trim();
        entity.Description = record.Description?.Trim();
        
        _db.Departments.Update(entity);
        _db.SaveChanges();
        return Success("Department is updated successfully.");
    }

    public ServiceBase Delete(int id)
    {
        Department department = _db.Departments.Include(d => d.Employees).SingleOrDefault(d => d.Id == id);
        if(department is null)
            return Error("Department is not found.");
        if(department.Employees.Any())
            return Error("There are employees associated with this department.");
        
        _db.Departments.Remove(department);
        _db.SaveChanges();
        return Success("Department is deleted successfully.");
    }
}