using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Models.Reponses
{
    public class SingleOrderLookUpResponse : Response
    {
        public Order order { get; set; }
    }
}
