using BaseballLeague.Data.Interfaces;
using BaseballLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Data.TestRepository
{
    public class MockBaseballRepository : IBaseballRepository
    {
        public static List<League> _leagues = new List<League>();


        public MockBaseballRepository()
        {
            if (!_leagues.Any())
            {
                _leagues = new List<League>()
                {
                    new League()
                    {
                        LeagueID = 1,
                        Name = "American League",
                        Teams = new List<Team>()
                        {
                            new Team()
                            {
                                TeamID = 1,
                                Manager = "Paul Molitor",
                                Name = "Minnesota Twins",
                                Players = new List<Player>()
                                {
                                    new Player()
                                    {
                                        PlayerID = 1,
                                        FName = "Ervin",
                                        LName = "Santana",
                                        PlayerPosition = Position.P,
                                        JerseyNum = 54,
                                        BattingAVG = .118M,
                                        YearsPlayed = 11,
                                        TeamID = 1
                                    },

                                    new Player()
                                    {
                                        PlayerID = 2,
                                        FName = "Brian",
                                        LName = "Dozier",
                                        PlayerPosition = Position.SB,
                                        JerseyNum = 2,
                                        BattingAVG = .268M,
                                        YearsPlayed = 4,
                                        TeamID = 1
                                    },

                                    new Player()
                                    {
                                        PlayerID = 3,
                                        FName = "Joe",
                                        LName = "Mauer",
                                        PlayerPosition = Position.FB,
                                        JerseyNum = 7,
                                        BattingAVG = .261M,
                                        YearsPlayed = 12,
                                        TeamID = 1
                                    }
                                }
                            },

                             new Team()
                            {
                                TeamID = 3,
                                Manager = "Terry Francona",
                                Name = "Hank Flecks",
                                Players = new List<Player>()
                                {
                                    new Player()
                                    {
                                        PlayerID = 7,
                                        FName = "Nathaniel",
                                        LName = "Betzest",
                                        PlayerPosition = Position.DH,
                                        JerseyNum = 10,
                                        BattingAVG = .379M,
                                        YearsPlayed = 15,
                                        TeamID = 3
                                    }
                                }
                             }
                        }
                    },

                new League()
                {
                    LeagueID = 2,
                    Name = "National League",
                    Teams = new List<Team>()
                        {
                            new Team()
                            {
                                TeamID = 2,
                                Manager = "Craig Counsell",
                                Name = "Milwaukee Brewers",
                                Players = new List<Player>()
                                {
                                    new Player()
                                    {
                                        PlayerID = 4,
                                        FName = "Jimmy",
                                        LName = "Nelson",
                                        PlayerPosition = Position.P,
                                        JerseyNum = 52,
                                        BattingAVG = .102M,
                                        YearsPlayed = 3,
                                        TeamID = 2
                                    },

                                        new Player()
                                    {
                                        PlayerID = 5,
                                        FName = "Ryan",
                                        LName = "Braun",
                                        PlayerPosition = Position.LF,
                                        JerseyNum = 8,
                                        BattingAVG = .305M,
                                        YearsPlayed = 9,
                                        TeamID = 2
                                    },
                                            new Player()
                                    {
                                        PlayerID = 6,
                                        FName = "Chris",
                                        LName = "Carter",
                                        PlayerPosition = Position.FB,
                                        JerseyNum = 33,
                                        BattingAVG = .222M,
                                        YearsPlayed = 6,
                                        TeamID = 2
                                    },
                                }
                            }
                        }
                    }
                };
            }
        }

        public List<League> GetAllLeagues()
        {
            return _leagues;
        }

        public League GetLeagueById(int leagueId)
        {
            return GetAllLeagues().FirstOrDefault(l => l.LeagueID == leagueId);
        }

        public void AddLeague(League newLeague)
        {
            newLeague.LeagueID = _leagues.Max(c => c.LeagueID) + 1;
            newLeague.Teams = new List<Team>();
            _leagues.Add(newLeague);
        }

        public void DeleteLeague(int leagueId)
        {
            _leagues.RemoveAll(l => l.LeagueID == leagueId);
        }

        public void EditLeague(League editedLeague)
        {
            var selectedLeague = _leagues.First(l => l.LeagueID == editedLeague.LeagueID);
            selectedLeague.Name = editedLeague.Name;
        }

        public List<Team> GetAllTeams()
        {
            List<Team> allTeams = new List<Team>();

            foreach (var league in _leagues)
            {
                var teamCheck = league.Teams.Any();
                if (teamCheck)
                {
                    foreach (var team in league.Teams)
                    {
                        allTeams.Add(team);
                    }
                }
            }
            return allTeams;
        }

        public Team GetTeamById(int teamId)
        {
            return GetAllTeams().FirstOrDefault(t => t.TeamID == teamId);
        }

        public void AddTeam(Team newTeam)
        {
            newTeam.TeamID = GetAllTeams().Max(c => c.TeamID) + 1;
            var leagueOfNewTeam = _leagues.FirstOrDefault(t => t.LeagueID == newTeam.LeagueID);
            leagueOfNewTeam.Teams.Add(newTeam);
        }

        public void EditTeam(Team editedTeam)
        {
            _leagues = _leagues.Select(l =>
            {
                l.Teams = l.Teams.Where(t => t.TeamID != editedTeam.TeamID).ToList();
                return l;
            })
            .ToList();

            _leagues = _leagues.Select(l =>
            {
                if (l.LeagueID == editedTeam.LeagueID)
                {
                    l.Teams.Add(editedTeam);
                }
                return l;
            })
            .ToList();
        }

        public void DeleteTeam(int teamId)
        {
            foreach (var league in _leagues)
            {
                league.Teams.FirstOrDefault(t => t.TeamID == teamId);
                league.Teams.RemoveAll(t => t.TeamID == teamId);
            }
        }
        public List<Player> GetAllPlayers()
        {
            var players = new List<Player>();
            var teams = GetAllTeams();
            foreach (var team in teams)
            {
                foreach (var player in team.Players)
                {
                    players.Add(player);
                }
            }
            return players;
        }

        public List<Player> GetPlayersByTeamId(int teamId)
        {
            var teams = GetAllTeams();
            var team = teams.FirstOrDefault(t => t.TeamID == teamId);
            return team.Players;
        }
        public Player GetPlayerById(int playerId)
        {
            var players = GetAllPlayers();
            var player = players.FirstOrDefault(p => p.PlayerID == playerId);
            return player;
        }
        public void AddPlayer(Player newPlayer)
        {
            newPlayer.PlayerID = GetAllPlayers().Max(c => c.PlayerID) + 1;
            var NewPlayerTeam = GetAllTeams().FirstOrDefault(p => p.TeamID == newPlayer.TeamID);
            NewPlayerTeam.Players.Add(newPlayer);
        }

        public void DeletePlayer(Player Player)
        {
            var team = _leagues.SelectMany(s => s.Teams).FirstOrDefault(t => t.TeamID == Player.TeamID);
            team.Players.RemoveAll(p => p.PlayerID == Player.PlayerID);
        }
    }
}


