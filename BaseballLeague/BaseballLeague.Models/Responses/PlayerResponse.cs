using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Models.Responses
{
    public class PlayerResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Player Player { get; set; }
    }
}
