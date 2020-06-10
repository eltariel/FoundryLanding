namespace FoundryLanding.Models.Data
{
    public class Player
    {
        public Player(User user, World world, string name, string playerHash, string password)
        {
            User = user;
            World = world;
            Name = name;
            PlayerHash = playerHash;
            Password = password;
        }

        public User User { get; }
        public World World { get; }
        
        public string Name { get;  }
        public string PlayerHash { get; }
        public string Password { get; }
    }
}