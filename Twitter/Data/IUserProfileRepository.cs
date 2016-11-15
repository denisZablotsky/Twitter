using Twitter.Models;

namespace Twitter.Data
{
    public interface IUserProfileRepository
    {
        UserProfile CreateUserProfile(UserProfile userProfile);
    }
}
