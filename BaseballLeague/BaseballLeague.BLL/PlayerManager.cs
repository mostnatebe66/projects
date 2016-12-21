using BaseballLeague.Models;
using BaseballLeague.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseballLeague.BLL
{
    public class PlayerManager
    {
        public PlayerListResponse GetAllPlayers()
        {
            var repo = Factory.CreateBaseballRepository();
            var response = new PlayerListResponse();
            var players = repo.GetAllPlayers();
            if (players.Count == 0)
            {
                response.Success = false;
                response.Message = $"No players found";
            }
            else
            {
                response.Success = true;
                response.Players = players;
            }
            return response;
        }

        public PlayerListResponse GetAllPlayersByTeamId(int teamId)
        {
            var repo = Factory.CreateBaseballRepository();
            var response = new PlayerListResponse();
            var players = repo.GetPlayersByTeamId(teamId);
            if (players.Count == 0)
            {
                response.Success = false;
                response.Message = $"No players found for that team id";
            }
            else
            {
                response.Success = true;
                response.Players = players;
            }
            return response;
        }

        public PlayerResponse GetPlayerById(int id)
        {
            var repo = Factory.CreateBaseballRepository();
            var response = new PlayerResponse();
            var player = repo.GetPlayerById(id);
            if (player == null)
            {
                response.Success = false;
                response.Message = $"no player found for that id";
            }
            else
            {
                response.Success = true;
                response.Player = player;
            }
            return response;
        }

        public void Add(Player playerToAdd)
        {
            var repo = Factory.CreateBaseballRepository();
            repo.AddPlayer(playerToAdd);
        }

        public bool Delete(int id)
        {
            Player player = GetAllPlayers().Players.SingleOrDefault(p => p.PlayerID == id);
            var repo = Factory.CreateBaseballRepository();
            var response = new PlayerResponse();
            if (player == null)
            {
                response.Success = false;
                response.Message = $"no player found for that id";
                return false;
            }
            repo.DeletePlayer(player);
            return true;
        }

        public TradeResponse TradePlayer(int playerOneId, int playerTwoId)
        {
            var tradeResponse = new TradeResponse();
            PlayerResponse response = GetPlayerById(playerOneId);
            Player player1;
            Player player2;

            if (response.Success)
            {
                player1 = response.Player;
            }
            else
            {
                tradeResponse.Message = $"Player 1 not found";
                return tradeResponse;
            }
            response = GetPlayerById(playerTwoId);
            if(response.Success)
            {
                player2 = response.Player;
            }
            else
            {
                tradeResponse.Message = $"player 2 not found";
                return tradeResponse;
            }

            var p1Team = player1.TeamID;
            var p1TeamName = player1.TeamName;
            player1.TeamID = player2.TeamID;
            player1.TeamName = player2.TeamName;
            player2.TeamID = p1Team;
            player2.TeamName = p1TeamName;
            Add(player1);
            Add(player2);

            Delete(playerOneId);
            Delete(playerTwoId);

            tradeResponse.Success = true;
            tradeResponse.Players[0] = player1;
            tradeResponse.Players[1] = player2;

            return tradeResponse;
        }
    }
}
