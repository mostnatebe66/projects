using BaseballLeague.Models;
using BaseballLeague.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.BLL
{
    public class TeamManager
    {
        public TeamListResponse GetAll()
        {
            var repo = Factory.CreateBaseballRepository();
            var response = new TeamListResponse();
            var teams = repo.GetAllTeams();

            if (teams.Count == 0)
            {
                response.Success = false;
                response.Message = $"No teams found";
            }
            else
            {
                response.Success = true;
                response.Teams = teams;
            }
            return response;
        }

        public TeamResponse GetTeamById(int teamId)
        {

            var repo = Factory.CreateBaseballRepository();
            var response = new TeamResponse();
            var team = repo.GetTeamById(teamId);
            if (team == null)
            {
                response.Success = false;
                response.Message = $"No team found for that id";
            }
            else
            {
                response.Success = true;
                response.Team = team;
            }
            return response;
        }

        public void AddTeam(Team teamToAdd)
        {
            var repo = Factory.CreateBaseballRepository();
            repo.AddTeam(teamToAdd);
        }

        public void Edit(Team teamToEdit)
        {
            var repo = Factory.CreateBaseballRepository();
            repo.EditTeam(teamToEdit);
        }

        public void Delete(int id)
        {
            var repo = Factory.CreateBaseballRepository();

            var team = GetTeamById(id);
            repo.DeleteTeam(id);
        }
    }
}
