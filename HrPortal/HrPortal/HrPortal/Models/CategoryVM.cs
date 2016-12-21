using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HrPortal.Models
{
    public class CategoryVM
    {
        public List<SelectListItem> Categories { get; set; }
        public Category category { get; set; }

        public CategoryVM(List<Category> categoriesToAdd)
        {
            Categories = categoriesToAdd.Select(c => new SelectListItem { Text = c.Name }).ToList();
        }

        public CategoryVM()
        {

        }
    }
}