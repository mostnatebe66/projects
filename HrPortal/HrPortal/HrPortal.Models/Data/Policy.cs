using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Models.Data
{
    public class Policy
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Name the Policy")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
    }
}
