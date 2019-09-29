using System.Collections.Generic;
using System.Linq;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerProvider _playerProvider;

        public PlayerService(IPlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        }

        public List<Player> GetPlayers()
        {
            return _playerProvider.GetPlayers().OrderBy(p => p.Id).ToList();
        }

        public Player GetPlayer(int id)
        {
            return _playerProvider.GetPlayer(id);
        }

        public bool DeletePlayer(int id)
        {
            var player = _playerProvider.GetPlayer(id);
            if (player == null)
                return false;

            _playerProvider.DeletePlayer(player);
            return true;
        }
    }
}
