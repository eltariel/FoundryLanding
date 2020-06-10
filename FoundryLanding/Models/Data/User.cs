using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace FoundryLanding.Models.Data
{
    public class User
    {
        public User(string userName, string discriminator, string email, string id)
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
        
        public List<World> Worlds { get; } = new List<World>();
        public List<Player> Players { get; } = new List<Player>();

        public static User MakeFromHeaders(IHeaderDictionary headers)
        {
            try
            {
                var userName = headers["X-Vouch-User"][0];
                var discriminator = headers["X-Vouch-IdP-Claims-Discriminator"][0];
                var email = headers["X-Vouch-IdP-Claims-Email"][0];
                var userid = headers["X-Vouch-IdP-Claims-Id"][0];

                var user = new User(userName, discriminator, email, userid);
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}