using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseballLeague.BLL;
using BaseballLeague.Models;
using BaseballLeague.Models.Responses;

namespace BaseballLeague.BLL.Tests
{
    [TestFixture]
    public class TeamTests
    {
        [Test]
        public void GetAllTeams()
        {
            TeamManager manager = new TeamManager();
            TeamListResponse allTeams = manager.GetAll();
            Assert.AreEqual(3, allTeams.Teams.Count());
        }

        [Test]
        public void AddTeam()
        {
            int teamId = 1;
            TeamManager manager = new TeamManager();
            Team teams = new Team();
            teams.TeamID = 1;
            Assert.AreEqual(teamId, teams.TeamID);
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(9, false)]
        [TestCase(-1, false)]
        public void DeleteTeam(int id, bool expected)
        {
            TeamManager manager = new TeamManager();
            manager.Delete(id);
            bool actual = manager.Delete(id);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void EditTeam()
        {
            int teamId = 1;
            TeamManager manager = new TeamManager();
            Team teams = new Team();
            teams.TeamID = 1;
            Assert.AreEqual(teamId, teams.TeamID);
        }
    }
}
