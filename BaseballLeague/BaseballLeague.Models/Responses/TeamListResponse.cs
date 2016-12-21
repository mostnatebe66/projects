﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Models.Responses
{
    public class TeamListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Team> Teams { get; set; }
    }
}
