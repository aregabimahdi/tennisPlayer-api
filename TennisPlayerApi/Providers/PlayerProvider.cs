using System.Collections.Generic;
using System.Linq;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.Providers
{
    public class PlayerProvider : IPlayerProvider
    {
        private Payload _playersPayLoad;

        public List<Player> Players
        {
            get
            {
                return _playersPayLoad.Players;
            }
        }

        public PlayerProvider(IDbContext dbContext)
        {
            _playersPayLoad = dbContext.GetContext();
        }

        public List<Player> GetPlayers()
        {
            return _playersPayLoad.Players;
        }

        public Player GetPlayer(int id)
        {
            return _playersPayLoad.Players.SingleOrDefault(p => p.Id == id);
        }

        public void DeletePlayer(Player player)
        {
            _playersPayLoad.Players.Remove(player);
        }
    }
}
