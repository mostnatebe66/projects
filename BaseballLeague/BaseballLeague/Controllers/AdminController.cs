using BaseballLeague.BLL;
using BaseballLeague.Models;
using BaseballLeague.Models.Responses;
using BaseballLeague.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseballLeague.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminLeagueList()
        {
            var manager = new LeagueManager();
            var response = new LeagueListResponse();
            response = manager.GetAll();
            return View(response.Leagues);
        }

        [HttpGet]
        public ActionResult AddLeague()
        {
            return View(new League());
        }

        [HttpPost]
        public ActionResult AddLeague(League newLeague)
        {
            if (ModelState.IsValid)
            {
                var manager = new LeagueManager();
                var response = new LeagueResponse();
                manager.Add(newLeague);
                return RedirectToAction("AdminLeagueList");
            }
            return View(newLeague);
        }

        [HttpGet]
        public ActionResult EditLeague(int leagueId)
        {
            var manager = new LeagueManager();
            var response = new LeagueResponse();
            response = manager.GetById(leagueId);
            return View(response.League);
        }

        [HttpPost]
        public ActionResult EditLeague(League editedLeague)
        {
            if (ModelState.IsValid)
            {
                var manager = new LeagueManager();
                manager.Edit(editedLeague);
                return RedirectToAction("AdminLeagueList");
            }
            return View(editedLeague);
        }

        [HttpGet]
        public ActionResult DeleteLeague(int LeagueId)
        {
            var manager = new LeagueManager();
            var league = manager.GetById(LeagueId);
            return View(league.League);
        }

        [HttpPost]
        public ActionResult DeleteLeague(League leagueDelete)
        {
            var manager = new LeagueManager();
            manager.Delete(leagueDelete.LeagueID);
            return RedirectToAction("AdminLeagueList");
        }

        [HttpGet]
        public ActionResult AdminTeamList()
        {
            var manager = new TeamManager();
            var response = new TeamListResponse();
            response = manager.GetAll();
            return View(response.Teams);
        }

        [HttpGet]
        public ActionResult AddTeam()
        {
            var repo = Factory.CreateBaseballRepository();
            var addTeamVm = new AddTeamVM();
            addTeamVm.SetLeagues(repo.GetAllLeagues());
            return View(addTeamVm);
        }

        [HttpPost]
        public ActionResult AddTeam(AddTeamVM teamToAdd)
        {
            if (ModelState.IsValid)
            {
                teamToAdd.Team.Players = new List<Player>();
                var manager = new TeamManager();
                manager.AddTeam(teamToAdd.Team);
                return RedirectToAction("AdminTeamList");
            }
            return View(teamToAdd);
        }

        [HttpGet]
        public ActionResult EditTeam(int teamId)
        {
            var teamToEdit = new AddTeamVM();
            var repo = Factory.CreateBaseballRepository();
            var manager = new TeamManager();
            var response = manager.GetTeamById(teamId);
            teamToEdit.Team = response.Team;
            teamToEdit.SetLeagues(repo.GetAllLeagues());
            return View(teamToEdit);
        }

        [HttpPost]
        public ActionResult EditTeam(AddTeamVM teamToEdit)
        {
            if (ModelState.IsValid)
            {
                var team = teamToEdit.Team;
                var manager = new TeamManager();
                manager.Edit(team);
                return RedirectToAction("AdminTeamList");
            }
            return View(teamToEdit);
        }

        [HttpGet]
        public ActionResult DeleteTeam(int teamId)
        {
            var manager = new TeamManager();
            var team = manager.GetTeamById(teamId);
            return View(team.Team);
        }

        [HttpPost]
        public ActionResult DeleteTeam(Team teamToDelete)
        {
            var manager = new TeamManager();
            manager.Delete(teamToDelete.TeamID);
            return RedirectToAction("AdminTeamList");
        }

        [HttpGet]
        public ActionResult AdminPlayerList()
        {
            var manager = new PlayerManager();
            var response = new PlayerListResponse();
            response = manager.GetAllPlayers();
            return View(response.Players);
        }

        public ActionResult AdminPlayerList(string searchString)
        {
            PlayerManager manager = new PlayerManager();
            var response = new PlayerListResponse();
            response = manager.GetAllPlayers();
            if (!String.IsNullOrEmpty(searchString))
            {
                var searchList = response.Players.Where(m => m.FName.Contains(searchString)).ToList();
                response.Players = searchList;
            }

            return View(response);
        }

        [HttpGet]
        public ActionResult AddPlayer()
        {
            var repo = Factory.CreateBaseballRepository();
            var addplayer = new PlayerVM();
            addplayer.SetTeams(repo.GetAllTeams());
            return View(addplayer);
        }

        [HttpPost]
        public ActionResult AddPlayer(PlayerVM newPlayer)
        {
            if (ModelState.IsValid)
            {
                var manager = new PlayerManager();
                manager.Add(newPlayer.Player);
                return RedirectToAction("AdminPlayerList");
            }
            return View(newPlayer);
        }

        [HttpGet]
        public ActionResult DeletePlayer(int playerID)
        {
            var manager = new PlayerManager();
            var player = manager.GetPlayerById(playerID);
            return View(player.Player);
        }

        [HttpPost]
        public ActionResult DeletePlayer(Player playerToDelete)
        {
            var manager = new PlayerManager();
            manager.Delete(playerToDelete.PlayerID);
            return RedirectToAction("AdminPlayerList");
        }
        
        [HttpGet]
        public ActionResult AdminTradeTeamListSelect()
        {
            var repo = Factory.CreateBaseballRepository();
            var vm = new TeamPlayerTradeVM();
            vm.SetTeam1(repo.GetAllTeams());
            vm.SetTeam2(repo.GetAllTeams());

            return View(vm);
        }

        [HttpPost]
        public ActionResult AdminTradeTeamListSelect(TeamPlayerTradeVM teams)
        {
            var repo = Factory.CreateBaseballRepository();
            var vm = new PlayerTradeVM(teams.Team1, teams.Team2);

            return View("AdminSelectPlayersToTrade", vm);
        }

        [HttpPost]
        public ActionResult AdminSelectPlayersToTrade(PlayerTradeVM players)
        {
            var manager = new PlayerManager();
            var response = manager.TradePlayer(players.P1, players.P2);
            return RedirectToAction("AdminLeagueList");
        }

    }
}