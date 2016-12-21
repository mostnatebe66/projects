using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HrPortal.Models
{
    public class PolicyVM
    {
        public List<SelectListItem> Categories { get; set; }
        public Policy Policy { get; set; }
        
        public PolicyVM(List<Category> categoriesToAdd)
        {
            Categories = categoriesToAdd.Select(c => new SelectListItem { Text = c.Name }).ToList();
        }      

        public PolicyVM()
        {

        }
    }
}