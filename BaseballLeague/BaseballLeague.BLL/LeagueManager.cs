using BaseballLeague.Models;
using BaseballLeague.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.BLL
{
    public class LeagueManager
    {
        public LeagueListResponse GetAll()
        {
            var repo = Factory.CreateBaseballRepository();
            var response = new LeagueListResponse();
            var leagues = repo.GetAllLeagues();
            if (leagues.Count == 0)
            {
                response.Success = false;
                response.Message = $"No league found with that ID";
            }
            else
            {
                response.Success = true;
                response.Leagues = leagues;
            }
            return response;
        }

        public LeagueResponse GetById(int id)
        {
            var repo = Factory.CreateBaseballRepository();
            var response = new LeagueResponse();
            var league = repo.GetLeagueById(id);
            if (league == null)
            {
                response.Success = false;
                response.Message = $"No league found for that id";
            }
            else
            {
                response.Success = true;
                response.League = league;
            }
            return response;
        }

        public void Add(League leagueToAdd)
        {
            var repo = Factory.CreateBaseballRepository();
            var response = new LeagueResponse();
            repo.AddLeague(leagueToAdd);
        }

        public void Edit(League leagueToEdit)
        {
            var repo = Factory.CreateBaseballRepository();
            repo.EditLeague(leagueToEdit);
        }

        public bool Delete(int id)
        {
            var repo = Factory.CreateBaseballRepository();

            var league = GetById(id);

            repo.DeleteLeague(id);

            if (league != GetById(id))
            {
                return false;
            }
            else
            {
                repo.DeleteLeague(id);
            }
            return true;

        }
    }
}
