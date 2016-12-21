using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrPortal.Models
{
    public class ViewPoliciesViewModel
    {
        public IEnumerable<Policy> Policies { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}