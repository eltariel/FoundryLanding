using System;
using System.Collections.Generic;

namespace FoundryLanding.Models.Data
{
    public class World
    {
        public World(IEnumerable<User> owners, string name, string hostName, string path)
        {
            Owners = new List<User>(owners);
            Name = name;
            HostName = hostName;
            Path = path;
        }

        public IEnumerable<User> Owners { get; }
        
        public string Name { get; }
        public string HostName { get; }
        public string Path { get; }

        public List<User> Users { get; } = new List<User>();
        public List<Player> Players { get; } = new List<Player>();
    }
}
