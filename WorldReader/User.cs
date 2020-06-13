using Newtonsoft.Json.Linq;

namespace WorldReader
{
    public class User
    {
        private User(string id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }

        public string Id { get; }
        public string Name { get; }
        public string Password { get; }

        public static User Parse(string line)
        {
            var j = JObject.Parse(line);

            var id = (string) j["_id"];
            var name = (string) j["name"];
            var password = (string) j["password"];
            
            return new User(id, name, password);
        }
    }
}