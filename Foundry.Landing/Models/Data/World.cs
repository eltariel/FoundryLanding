using System.Collections.Generic;

namespace FoundryLanding.Models.Data
{
    public class World
    {
        public World(IEnumerable<DiscordUser> owners, string name, string hostName, string path)
        {
            Owners = new List<DiscordUser>(owners);
            Name = name;
            HostName = hostName;
            Path = path;
        }

        public IEnumerable<DiscordUser> Owners { get; }
        
        public string Name { get; }
        public string HostName { get; }
        public string Path { get; }

        public List<DiscordUser> Users { get; } = new List<DiscordUser>();
        public List<Player> Players { get; } = new List<Player>();
    }
}
