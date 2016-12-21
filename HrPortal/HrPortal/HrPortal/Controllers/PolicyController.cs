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
    public class PolicyController : Controller
    {
        [HttpGet]
        public ActionResult ViewPolicies()
        {
            HrManager policyManager = PolicyManagerFactory.Create();
            var model = new ViewPoliciesViewModel();
            model.Categories = policyManager.LookUpCategories();
            return View(model);
        }

        [HttpPost]
        public ActionResult ViewPolicies(string CategoryID)
        {
            HrManager policyManager = PolicyManagerFactory.Create();
            var model = new ViewPoliciesViewModel();
            model.Categories = policyManager.LookUpCategories();
            model.Policies = policyManager.LookUpPolicies().Where(c => c.CategoryID == CategoryID);
            return View(model);
        }

        [HttpGet]
        public ActionResult ManagePolicies()
        {
            HrManager policyManager = PolicyManagerFactory.Create();
            var model = policyManager.LookUpPolicies();
            return View(model);
        }

        [HttpGet]
        public ActionResult AddPolicy()
        {
            List<Category> categories = new List<Category>();
            HrManager policyManager = PolicyManagerFactory.Create();
            categories = policyManager.LookUpCategories().ToList();
            PolicyVM policy = new PolicyVM(categories);
            return View(policy);
        }

        [HttpPost]
        public ActionResult AddPolicy(PolicyVM policyVM, string content, string CategoryID)
        {
            if (ModelState.IsValid)
            {
                HrManager manager = PolicyManagerFactory.Create();
                policyVM.Policy.Description = content;
                policyVM.Policy.CategoryID = CategoryID;
                manager.AddPolicies(policyVM.Policy);
                return RedirectToAction("Index", "Policy/ManagePolicies");
            }
            return View(policyVM);
        }

        [HttpGet]
        public ActionResult DeletePolicy(int ID)
        {
            Policy policy = new Policy();
            policy.ID = ID;
            return View(policy);
        }

        [HttpPost]
        public ActionResult DeletePolicy(Policy policyVM, int ID)
        {
            HrManager delete = PolicyManagerFactory.Create();
            policyVM.ID = ID;
            delete.DeletePolicies(policyVM.ID);
            return RedirectToAction("Index", "Policy/ManagePolicies");
        }
        [HttpGet]
        public ActionResult ViewPolicy(int id)
        {
            HrManager manager = PolicyManagerFactory.Create();
            Policy model = manager.GetPolicy(id);
            return View(model);
        }

    }
}