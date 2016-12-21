using BaseballLeague.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseballLeague.Models;
using System.Data.SqlClient;
using Dapper;

namespace BaseballLeague.Data.TestRepository
{
    public class DapperRepository : IBaseballRepository
    {

        public List<League> GetAllLeagues()
        {
            List<League> leagues = new List<League>();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                leagues = cn.Query<League>(@"select LeagueID, Name from League").ToList();
            }

            if (leagues.Count != 0)
            {
                foreach (var league in leagues)
                {
                    league.Teams = GetAllTeamsByLeagueId(league.LeagueID);
                }
            }
            return leagues;
        }

        public League GetLeagueById(int leagueId)
        {
            League league = new League();
            try
            {
                using (var cn = new SqlConnection(Settings.ConnectionString))
                {
                    var leagueID = new DynamicParameters();
                    leagueID.Add("LeagueID", leagueId);
                    league = cn.Query<League>(@"select LeagueID, Name from League where LeagueID = @LeagueID", leagueID).FirstOrDefault();
                }
                league.Teams = GetAllTeamsByLeagueId(leagueId);
            }
            catch (Exception)
            {
                return league;
            }
            return league;
        }

        public void AddLeague(League newLeague)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var name = new DynamicParameters();
                name.Add("Name", newLeague.Name);
                cn.Execute(@"insert into League (Name) values (@Name)", name);
            }
        }

        public void DeleteLeague(int leagueId)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var leagueID = new DynamicParameters();
                leagueID.Add("LeagueID", leagueId);
                cn.Execute(@"delete from League where LeagueID = @leagueID", leagueID);
            }
        }

        public void EditLeague(League editedLeague)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute(@"update League set Name = @NAME where LeagueID = @LEAGUEID", new
                {
                    LEAGUEID = editedLeague.LeagueID,
                    NAME = editedLeague.Name
                });
            }
        }

        public List<Team> GetAllTeams()
        {
            List<Team> teams = new List<Team>();
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                teams = cn.Query<Team>(@"select TeamID, Name, Manager, LeagueID from Team").ToList();
            }

            if (teams.Count != 0)
            {
                foreach (var team in teams)
                {
                    team.Players = GetPlayersByTeamId(team.TeamID);
                }
            }

            return teams;
        }

        public List<Team> GetAllTeamsByLeagueId(int leagueId)
        {
            List<Team> teams = new List<Team>();
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                teams = cn.Query<Team>(@"select TeamID, Name, Manager, LeagueID from Team where LeagueID = @LEAGUEID", new { LEAGUEID = leagueId }).ToList();
            }

            if (teams.Count != 0)
            {
                foreach (var team in teams)
                {
                    team.Players = GetPlayersByTeamId(team.TeamID);
                }
            }

            return teams;
        }

        public Team GetTeamById(int teamId)
        {
            Team team = new Team();
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                team = cn.Query<Team>(@"select TeamID, Name, Manager, LeagueID from Team where TeamID = @TeamID", new { TeamID = teamId }).FirstOrDefault();
            }

            var teamPlayers = GetPlayersByTeamId(teamId);
            if (teamPlayers.Count != 0)
            {
                team.Players = teamPlayers;
            }

            return team;
        }

        public void AddTeam(Team newTeam)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute(@"insert into Team (Name, Manager, LeagueID) values (@NAME, @MANAGER, @LEAGUEID)", new
                {
                    NAME = newTeam.Name,
                    MANAGER = newTeam.Manager,
                    LEAGUEID = newTeam.LeagueID,
                });
            }
        }

        public void EditTeam(Team editedTeam)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute(@"update Team set Name = @NAME, Manager = @MANAGER, LeagueID = @LEAGUEID where TeamID = @TEAMID", new
                {
                    NAME = editedTeam.Name,
                    MANAGER = editedTeam.Manager,
                    LEAGUEID = editedTeam.LeagueID,
                    TEAMID = editedTeam.TeamID
                });
            }
        }

        public void DeleteTeam(int teamId)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute(@"delete from Team where TeamID = @TEAMID", new { TEAMID = teamId });
            }
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                players = cn.Query<Player>(@"select PlayerID, FName, LName, JerseyNum, PlayerPosition, BattingAVG, YearsPlayed, TeamID, TeamName from Player").ToList();
            }
            return players;
        }

        public List<Player> GetPlayersByTeamId(int teamId)
        {
            List<Player> players = new List<Player>();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                players = cn.Query<Player>(@"select PlayerID, FName, LName, JerseyNum, PlayerPosition, BattingAVG, YearsPlayed, TeamID, TeamName from Player where TeamName = @TeamName", new { TeamName = teamId }).ToList();
            }
            return players;
        }

        public Player GetPlayerById(int playerId)
        {
            Player player = new Player();

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                player = cn.Query<Player>(@"select PlayerID, FName, LName, JerseyNum, PlayerPosition, BattingAVG, YearsPlayed, TeamID, TeamName from Player where PlayerID = @PlayerId", new { PlayerId = playerId }).FirstOrDefault();
            }

            return player;
        }

        public void AddPlayer(Player newPlayer)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute(@"insert into Player (FName, LName, JerseyNum, PlayerPosition, BattingAVG, YearsPlayed, TeamID, TeamName)
                                    values (@FNAME, @LNAME, @JERSEYNUM, @PLAYERPOSITION, @BATTINGAVG, @YEARSPLAYED, @TEAMID, @TEAMNAME)", new
                {
                    FNAME = newPlayer.FName,
                    LNAME = newPlayer.LName,
                    JERSEYNUM = newPlayer.JerseyNum,
                    PLAYERPOSITION = newPlayer.PlayerPosition,
                    BATTINGAVG = newPlayer.BattingAVG,
                    YEARSPLAYED = newPlayer.YearsPlayed,
                    TEAMID = newPlayer.TeamID,
                    TEAMNAME = newPlayer.TeamName
                });
            }
        }

        public void DeletePlayer(Player player)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute(@"delete from Player where PlayerID = @PLAYERID", new { PLAYERID = player.PlayerID });
            }
        }
    }
}
