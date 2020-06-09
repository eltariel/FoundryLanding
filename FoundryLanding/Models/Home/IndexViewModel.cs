using FoundryLanding.Models.Data;

namespace FoundryLanding.Models.Home
{
    public class IndexViewModel
    {
        public IndexViewModel(UserModel userModel)
        {
            UserModel = userModel;
        }

        public UserModel UserModel { get; }
    }
}