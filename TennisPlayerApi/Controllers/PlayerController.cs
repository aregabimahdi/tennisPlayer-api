using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService) => _playerService = playerService;

        /// <summary>
        /// Get all players 
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            var players = _playerService.GetPlayers();

            return Ok(players);
        }

        /// <summary>
        /// Get player by id 
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayer(int id)
        {
            var player = _playerService.GetPlayer(id);
            if (player == null)
                return NotFound();

            return Ok(player);
        }

        /// <summary>
        /// Delete player by id 
        /// </summary> 
        [HttpDelete("{id}")]
        public ActionResult DeletePlayer(int id)
        {
            var success = _playerService.DeletePlayer(id);
            if (success)
                return Ok();

            return NotFound();
        }
    }
}
