using System.Collections.Generic;
using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.Interfaces
{
    public interface IPlayerProvider
    {
        List<Player> GetPlayers();

        Player GetPlayer(int id);

        void DeletePlayer(Player player);
    }
}
