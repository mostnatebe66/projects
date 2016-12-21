using BaseballLeague.BLL;
using BaseballLeague.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseballLeague.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeamDetails(int id)
        {
            var manager = new TeamManager();
            var response = new TeamResponse();
            response = manager.GetTeamById(id);
            return View(response.Team);
        }

        public ActionResult PlayerDetails(int id)
        {
            var manager = new PlayerManager();
            var response = new PlayerResponse();
            response = manager.GetPlayerById(id);
            return View(response.Player);
        }
    }
}