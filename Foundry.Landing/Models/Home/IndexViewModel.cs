using FoundryLanding.Models.Data;

namespace FoundryLanding.Models.Home
{
    public class IndexViewModel
    {
        public IndexViewModel(DiscordUser discordUser)
        {
            DiscordUser = discordUser;
        }

        public DiscordUser DiscordUser { get; }
    }
}