using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class EmployeeController : MvcController
    {
        // Service injections:
        private readonly IService<Employee, EmployeeModel> _employeeService;
        private readonly IService<Department, DepartmentModel> _departmentService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public EmployeeController(
			IService<Employee, EmployeeModel> employeeService
            , IService<Department, DepartmentModel> departmentService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _employeeService = employeeService;
            _departmentService = departmentService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Employee
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _employeeService.Query().ToList();
            return View(list);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _employeeService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["DepartmentId"] = new SelectList(_departmentService.Query().ToList(), "Record.Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _employeeService.Create(employee.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = employee.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(employee);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _employeeService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Employee/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _employeeService.Update(employee.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = employee.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(employee);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _employeeService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Employee/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _employeeService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
