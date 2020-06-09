namespace FoundryLanding.Models.Data
{
    public class UserModel
    {
        public UserModel(string userName, string discriminator, string email, string id)
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
    }
}