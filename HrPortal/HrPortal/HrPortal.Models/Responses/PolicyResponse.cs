using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Models.Responses
{
    public class PolicyResponse : Response
    {
        public Policy policy { get; set; }
    }
}
