using BaseballLeague.BLL;
using BaseballLeague.Data.TestRepository;
using BaseballLeague.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseballLeague.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var manager = new LeagueManager();
            var response = new LeagueListResponse();
            response = manager.GetAll();
            return View(response.Leagues);
        }
    }
}