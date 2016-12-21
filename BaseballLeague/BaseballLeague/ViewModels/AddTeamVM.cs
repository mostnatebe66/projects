using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseballLeague.Models
{
    public class AddTeamVM
    {
        public Team Team { get; set; }
        public List<SelectListItem> Leagues { get; set; }


        public AddTeamVM()
        {
            Team = new Team();
            Leagues = new List<SelectListItem>();
        }

        public void SetLeagues(IEnumerable<League> leagues)
        {
            foreach (var league in leagues)
            {
                Leagues.Add(new SelectListItem()
                {
                    Value = league.LeagueID.ToString(),
                    Text = league.Name
                });
            }
        }
    }
}