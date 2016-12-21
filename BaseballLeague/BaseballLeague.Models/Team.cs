using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        [Required(ErrorMessage = "Put Team Name here")]
        [StringLength(maximumLength: 40)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Put Manager Name here")]
        [StringLength(maximumLength: 40)]
        public string Manager { get; set; }
        public List<Player> Players { get; set; }
        public int LeagueID { get; set; }
    }
}
