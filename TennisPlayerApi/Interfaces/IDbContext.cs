using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.Interfaces
{
    public interface IDbContext
    {
        Payload GetContext();
    }
}
