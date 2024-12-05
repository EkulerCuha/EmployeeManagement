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
        return _db.Employees.Include(e => e.Department).OrderByDescending(e => e.HireDate).ThenByDescending(e => e.IsFemale).ThenBy(e => e.FirstName).Select(e => new EmployeeModel() { Record = e });
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
        record.FirstName = record.FirstName.Trim();
        record.LastName = record.LastName.Trim();
        _db.Employees.Update(record);
        _db.SaveChanges();
        return Success("Employee updated");
    }

    public ServiceBase Delete(int id)
    {
        var employee = _db.Employees.Include(e => e.Department).SingleOrDefault(e => e.Id == id);
        if(employee is null)
            return Error("Employee not found");
        _db.Employees.Remove(employee);
        _db.SaveChanges();
        return Success("Employee deleted");
    }
    
}