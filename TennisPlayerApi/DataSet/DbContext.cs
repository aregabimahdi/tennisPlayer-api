using Newtonsoft.Json;
using System.IO;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.DataSet
{
    public class DbContext : IDbContext
    {
        private readonly string jsonFile = @".\DataSet\headtohead.json";

        public Payload GetContext()
        {
            using (StreamReader file = File.OpenText(jsonFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                var dataSet = (Payload)serializer.Deserialize(file, typeof(Payload));

                return dataSet;
            }
        }

        public void SaveContext(Payload payload)
        {
            string output = JsonConvert.SerializeObject(payload, Formatting.Indented);
            File.WriteAllText(jsonFile, output);
        }
    }
}
