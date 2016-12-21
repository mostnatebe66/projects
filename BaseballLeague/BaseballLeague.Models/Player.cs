using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        [Required(ErrorMessage = "Put First Name here")]
        [StringLength(maximumLength: 40)]
        public string FName { get; set; }
        [Required(ErrorMessage = "Put your Last Name here")]
        [StringLength(maximumLength: 40)]
        public string LName { get; set; }
        [Required(ErrorMessage = "Jersey Number here - 2 digit max")]
        [Range(1, 99)]
        public int JerseyNum { get; set; }
        [Required(ErrorMessage = "Put Player Position here - ex. FB, LF, C, etc.")]
        public Position PlayerPosition { get; set; }
        [Required(ErrorMessage = "Put batting ave here - .000 format")]
        [Range(.0, .999)]
        public decimal BattingAVG { get; set; }
        [Required(ErrorMessage = "Years Played here - 2 digit max")]
        [Range(0, 30)]
        public int? YearsPlayed { get; set; }
        public int TeamID { get; set; }
        [Required(ErrorMessage = "Put your Team Name here")]
        [StringLength(maximumLength: 40)]
        public string TeamName { get; set; }
    }
}
