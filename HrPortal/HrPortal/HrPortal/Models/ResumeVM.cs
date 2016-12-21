using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HrPortal.Models
{
    public class ResumeVM
    {
        public List<SelectListItem> Applications { get; set; }
        public Applicant Applicant { get; set; }

        public ResumeVM(List<Applicant> applicantsToAdd)
        {
            Applications = applicantsToAdd.Select(a => new SelectListItem { Text = a.FirstName }).ToList();
        }

        public ResumeVM()
        {

        }
    }
}