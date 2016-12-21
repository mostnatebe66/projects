﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Models.Responses
{
    public class TradeResponse
    {
        public TradeResponse()
        {
            Players = new Player[2];
        }
        public Player[] Players { get; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}