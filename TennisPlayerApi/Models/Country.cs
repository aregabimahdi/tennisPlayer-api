using Newtonsoft.Json;
using System;

namespace TennisPlayer.Api.Models
{
    public class Country
    {
        [JsonProperty("picture")]
        public Uri Picture { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
