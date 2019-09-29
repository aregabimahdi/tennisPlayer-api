using System.Collections.Generic;
using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.Interfaces
{
    public interface IPlayerService
    {
        List<Player> GetPlayers();

        Player GetPlayer(int id);

        bool DeletePlayer(int id);
    }
}
