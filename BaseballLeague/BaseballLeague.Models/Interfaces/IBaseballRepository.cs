using BaseballLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.Data.Interfaces
{
    public interface IBaseballRepository
    {
        List<League> GetAllLeagues();
        League GetLeagueById(int leagueId);
        void AddLeague(League newLeague);
        void DeleteLeague(int leagueId);
        void EditLeague(League editedLeague);

        List<Team> GetAllTeams();
        Team GetTeamById(int teamId);
        void AddTeam(Team newTeam);
        void EditTeam(Team editedTeam);
        void DeleteTeam(int teamId);

        List<Player> GetAllPlayers();
        List<Player> GetPlayersByTeamId(int teamId);
        Player GetPlayerById(int playerId);
        void AddPlayer(Player newPlayer);
        void DeletePlayer(Player Player);
    }
}
