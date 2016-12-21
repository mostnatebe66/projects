using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Models
{
    public class League
    {
        public int LeagueID { get; set; }
        [Required(ErrorMessage = "Put League Name here")]
        [StringLength(maximumLength: 40)]
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
    }
}
