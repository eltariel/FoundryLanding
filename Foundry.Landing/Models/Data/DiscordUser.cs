using System.Collections.Generic;
using System.Linq;
using Foundry.WorldReader;
using Microsoft.AspNetCore.Http;

namespace FoundryLanding.Models.Data
{
    public class DiscordUser
    {
        public DiscordUser(string userName, string discriminator, string email, string id)
        {
            UserName = userName;
            Discriminator = discriminator;
            Email = email;
            Id = id;
        }

        public string UserName { get; }
        public string Discriminator { get; }
        public string Email { get; }
        public string Id { get; }

        public List<World> Worlds => FoundryUsers.Select(u => u.World).Distinct().ToList();
        public List<User> FoundryUsers { get; } = new List<User>();

        public static DiscordUser MakeFromHeaders(IHeaderDictionary headers)
        {
            try
            {
                var userName = headers["X-Vouch-User"][0];
                var discriminator = headers["X-Vouch-IdP-Claims-Discriminator"][0];
                var email = headers["X-Vouch-IdP-Claims-Email"][0];
                var userid = headers["X-Vouch-IdP-Claims-Id"][0];

                var user = new DiscordUser(userName, discriminator, email, userid);
                return user;
            }
            catch
            {
                return new DiscordUser("Unknown User", "0000", "nobody@example.com", "no-id");
            }
        }
    }
}