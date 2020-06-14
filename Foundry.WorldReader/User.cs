﻿using Newtonsoft.Json.Linq;

namespace Foundry.WorldReader
{
    public class User
    {
        private User(string id, string name, string password, World world, string discordUser, bool deleted)
        {
            Id = id;
            Name = name;
            Password = password;
            World = world;
            DiscordUser = discordUser;
            IsDeleted = deleted;
        }

        public string Id { get; }
        public string Name { get; }
        public string Password { get; }
        public string DiscordUser { get; }
        public World World { get; }
        public bool IsDeleted { get; }

        public static User Parse(string line, World world)
        {
            var j = JObject.Parse(line);

            var id = (string) j["_id"];
            var name = (string) j["name"];
            var password = (string) j["password"];
            var discordUser = (string) j["flags"]?["world"]?["discord-user"] ?? "";

            var isDeleted = (bool)(j["$$deleted"] ?? false);
            
            return new User(id, name, password, world, discordUser, isDeleted);
        }
    }
}