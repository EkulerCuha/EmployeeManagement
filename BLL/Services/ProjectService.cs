using BLL.DAL;
using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Bases;

public class ProjectService : ServiceBase, IService<Project, ProjectModel>
{
    public ProjectService(Db db) : base(db) { }

    public IQueryable<ProjectModel> Query()
    {
        return _db.Projects.OrderByDescending(p => p.DueDate).ThenBy(p => p.StartDate).ThenBy(p => p.Name).Select(p => new ProjectModel() { Record = p });
    }

    public ServiceBase Create(Project record)
    {
        if (_db.Projects.Any(p => p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            return Error($"Project {record.Name} already exists.");

        record.Name = record.Name?.Trim();
        _db.Projects.Add(record);
        _db.SaveChanges();
        return Success($"Project {record.Name} has been created.");
    }

    public ServiceBase Update(Project record)
    {
        if(_db.Projects.Any(p => p.Id != record.Id && p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            return Error($"Project {record.Name} already exists.");
        
        var entity = _db.Projects.Find(record.Id);
        if(entity is null)
            return Error($"Project does not found.");
        
        _db.EmployeeProjects.RemoveRange(entity.EmployeeProjects);
        
        entity.Name = record.Name?.Trim();
        entity.StartDate = record.StartDate;
        entity.DueDate = record.DueDate;
        _db.Projects.Update(entity);
        _db.SaveChanges();
        return Success($"Project {record.Name} has been updated.");
    }

    public ServiceBase Delete(int id)
    {
        var entity = _db.Projects.Include(p => p.EmployeeProjects).SingleOrDefault(p => p.Id == id);
        if(entity is null)
            return Error($"Project {id} does not exists.");
        
        _db.EmployeeProjects.RemoveRange(entity.EmployeeProjects);
        _db.Projects.Remove(entity);
        _db.SaveChanges();
        return Success($"Project {id} has been deleted.");
    }
}