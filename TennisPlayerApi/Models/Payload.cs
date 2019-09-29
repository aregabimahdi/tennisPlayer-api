using Newtonsoft.Json;
using System.Collections.Generic;

namespace TennisPlayer.Api.Models
{
    public class Payload
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}
