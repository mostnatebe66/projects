using BaseballLeague.Models;
using BaseballLeague.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.BLL.Tests
{
    [TestFixture]
    public class LeagueTests
    {
        [Test]
        public void GetAllLeagues()
        {
            LeagueManager manager = new LeagueManager();
            LeagueListResponse allLeagues = manager.GetAll();
            Assert.AreEqual(2, allLeagues.Leagues.Count());
        }

        [Test]
        public void AddLeague()
        {
            int leagueId = 1;
            LeagueManager manager = new LeagueManager();
            League leagues = new League();
            leagues.LeagueID = 1;
            Assert.AreEqual(leagueId, leagues.LeagueID);
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(9, false)]
        [TestCase(-1, false)]
        public void DeleteLeague(int id, bool expected)
        {
            LeagueManager manager = new LeagueManager();
            manager.Delete(id);
            bool actual = manager.Delete(id);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void EditLeague()
        {
            int leagueId = 1;
            LeagueManager manager = new LeagueManager();
            League leagues = new League();
            leagues.LeagueID = 1;
            Assert.AreEqual(leagueId, leagues.LeagueID);
        }
    }
}
