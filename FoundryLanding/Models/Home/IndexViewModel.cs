using FoundryLanding.Models.Data;

namespace FoundryLanding.Models.Home
{
    public class IndexViewModel
    {
        public IndexViewModel(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}