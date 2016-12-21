using BaseballLeague.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.BLL.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void GetAllPlayers()
        {
            PlayerManager manager = new PlayerManager();
            PlayerListResponse allPlayers = manager.GetAllPlayers();
            Assert.AreEqual(7, allPlayers.Players.Count());
        }

        [Test]
        public void AddPlayer()
        {
            int playerId = 1;
            PlayerManager manager = new PlayerManager();
            Player player = new Player();
            player.PlayerID = 1;
            Assert.AreEqual(playerId, player.PlayerID);
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(45, false)]
        [TestCase(-1, false)]
        public void DeletePlayer(int id, bool expected)
        {
            LeagueManager manager = new LeagueManager();
            manager.Delete(id);
            bool actual = manager.Delete(id);
            Assert.AreEqual(actual, expected);
        }
    }
}
