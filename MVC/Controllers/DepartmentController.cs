using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers;
using BLL.Services;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class DepartmentController : MvcController
    {
        // Service injections:
        private readonly IService<Department, DepartmentModel> _departmentService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public DepartmentController(
			IService<Department, DepartmentModel> departmentService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _departmentService = departmentService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Department
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _departmentService.Query().ToList();
            return View(list);
        }

        // GET: Department/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _departmentService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _departmentService.Create(department.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = department.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(department);
        }

        // GET: Department/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _departmentService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _departmentService.Update(department.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = department.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(department);
        }

        // GET: Department/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _departmentService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Department/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _departmentService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
