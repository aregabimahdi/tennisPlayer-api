using Newtonsoft.Json;
using System.IO;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;

namespace TennisPlayer.Api.DataSet
{
    public class DbContext : IDbContext
    {
        public Payload GetContext()
        {
            using (StreamReader file = File.OpenText(@".\DataSet\headtohead.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                var dataSet = (Payload)serializer.Deserialize(file, typeof(Payload));

                return dataSet;
            }
        }
    }
}
