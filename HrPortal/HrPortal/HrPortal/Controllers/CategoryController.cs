using HrPortal.BLL;
using HrPortal.Models;
using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HrPortal.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult ViewCategories()
        {
            HrManager manager = PolicyManagerFactory.Create();
            var model = manager.LookUpCategories();
            return View(model);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            HrManager manager = PolicyManagerFactory.Create();
            CategoryVM categoryVM = new CategoryVM();
            return View(categoryVM);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                HrManager manager = PolicyManagerFactory.Create();
                manager.AddCategories(categoryVM.category.Name);
                var model = manager.LookUpCategories();
                return View("ViewCategories", model);
            }
            return View(categoryVM);
        }

        [HttpGet]
        public ActionResult DeleteCategory(string CategoryID)
        {
            HrManager manager = PolicyManagerFactory.Create();
            Category category = manager.GetCategory(CategoryID);
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteCategory(string Name, string nothing)
        {
            HrManager manager = PolicyManagerFactory.Create();
            Category category = manager.GetCategory(Name);
            manager.DeleteCategories(category.Name);
            return RedirectToAction("Index", "Category/ViewCategories");
        }

    }
}