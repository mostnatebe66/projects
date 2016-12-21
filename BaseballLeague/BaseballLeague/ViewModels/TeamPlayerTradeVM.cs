using BaseballLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseballLeague.ViewModels
{
    public class TeamPlayerTradeVM
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public List<SelectListItem> Teams1 { get; set; }
        public List<SelectListItem> Teams2 { get; set; }

        public TeamPlayerTradeVM()
        {
            Teams1 = new List<SelectListItem>();
            Team1 = new Team();
            Teams2 = new List<SelectListItem>();
            Team2 = new Team();
        }

        public void SetTeam1(IEnumerable<Team> teams)
        {
            foreach (var team in teams)
            {
                Teams1.Add(new SelectListItem()
                {
                    Value = team.TeamID.ToString(),
                    Text = team.Name
                });
            }
        }

        public void SetTeam2(IEnumerable<Team> teams)
        {
            foreach (var team in teams)
            {
                Teams2.Add(new SelectListItem()
                {
                    Value = team.TeamID.ToString(),
                    Text = team.Name
                });
            }
        }
    }
}