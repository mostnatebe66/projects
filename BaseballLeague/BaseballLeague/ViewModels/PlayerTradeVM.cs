using BaseballLeague.BLL;
using BaseballLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseballLeague.ViewModels
{
    public class PlayerTradeVM
    {
        public PlayerTradeVM()
        {
        }

        public PlayerTradeVM(Team teamOne, Team teamTwo)
        {
            var manager = new PlayerManager();
            TeamOneName = teamOne.Name;
            TeamTwoName = teamTwo.Name;
            TeamOnePlayers = TeamPlayers(manager.GetAllPlayersByTeamId(teamOne.TeamID).Players);
            TeamTwoPlayers = TeamPlayers(manager.GetAllPlayersByTeamId(teamTwo.TeamID).Players);
        }

        public string TeamOneName { get; set; }
        public string TeamTwoName { get; set; }
        public IEnumerable<SelectListItem> TeamOnePlayers { get; set; }
        public IEnumerable<SelectListItem> TeamTwoPlayers { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }


        public IEnumerable<SelectListItem> TeamPlayers(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                var item = new SelectListItem()
                {
                    Value = player.PlayerID.ToString(),
                    Text = player.FName + " " + player.LName
                };
                yield return item;
            }
        }
    }
}