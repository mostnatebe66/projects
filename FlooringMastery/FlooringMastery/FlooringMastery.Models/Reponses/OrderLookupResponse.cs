﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Reponses
{
    public class LookUpAllOrdersResponse : Response
    {
        public List<Order> Order { get; set;  }
    }
}
