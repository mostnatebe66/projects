using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseballLeague.Models
{
    public class PlayerVM
    {
        public Player Player { get; set; }
        public List<SelectListItem> Teams { get; set; }

        public PlayerVM()
        {
            Teams = new List<SelectListItem>();
            Player = new Player();
        }

        public void SetTeams(IEnumerable<Team> teams)
        {
            foreach (var team in teams)
            {
                Teams.Add(new SelectListItem()
                {
                    Value = team.TeamID.ToString(),
                    Text = team.Name
                });
            }
        }
    }
}