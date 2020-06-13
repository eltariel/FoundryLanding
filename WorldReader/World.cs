using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WorldReader
{
    public class World
    {
        public World(Host host, string worldPath)
        {
            Host = host;
            WorldPath = Path.Combine(host.DataPath, "Data", "worlds", worldPath);
        }
        
        public string WorldPath { get; }
        public Host Host { get; }

        public string Name { get; private set; }
        public string Title { get; private set; }
        public string GameSystem { get; private set; }

        public List<User> Users { get; } = new List<User>();

        public World Load()
        {
            var world = JObject.Parse(File.ReadAllText(Path.Combine(WorldPath, "world.json")));
            Name = (string) world["name"];
            Title = (string) world["title"];
            GameSystem = (string) world["system"];

            LoadUsers();
            return this;
        }

        private void LoadUsers()
        {
            var usersDb = File.ReadAllLines(Path.Combine(WorldPath, "data", "users.db"));
            foreach (var line in usersDb)
            {
                var u = User.Parse(line);
                Users.Add(u);
            }
        }
    }
}