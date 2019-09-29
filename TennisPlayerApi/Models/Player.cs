using Newtonsoft.Json;
using System;

namespace TennisPlayer.Api.Models
{
    public class Player
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("picture")]
        public Uri Picture { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
