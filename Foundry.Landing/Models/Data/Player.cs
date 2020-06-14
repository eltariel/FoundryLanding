namespace FoundryLanding.Models.Data
{
    public class Player
    {
        public Player(DiscordUser discordUser, World world, string name, string playerHash, string password)
        {
            DiscordUser = discordUser;
            World = world;
            Name = name;
            PlayerHash = playerHash;
            Password = password;
        }

        public DiscordUser DiscordUser { get; }
        public World World { get; }
        
        public string Name { get;  }
        public string PlayerHash { get; }
        public string Password { get; }
    }
}