using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Models.Data
{
    public class Category
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please name category")]
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
