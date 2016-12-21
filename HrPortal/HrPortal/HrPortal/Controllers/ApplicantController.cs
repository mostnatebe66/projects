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
    public class ApplicantController : Controller
    {
        [HttpGet]
        public ActionResult AddApplication()
        {
            List<Applicant> applications = new List<Applicant>();
            HrManager manager = PolicyManagerFactory.Create();
            ResumeVM resumeVM = new ResumeVM(applications);
            return View(resumeVM);
        }

        [HttpPost]
        public ActionResult AddApplication(ResumeVM resumeVM, Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                HrManager manager = PolicyManagerFactory.Create();
                manager.AddApplication(resumeVM.Applicant);
                return RedirectToAction("SubmittedApplication");
            }
            return View(resumeVM);
        }

        
        public ActionResult SubmittedApplication()
        {
            return View();
        }
    }
}