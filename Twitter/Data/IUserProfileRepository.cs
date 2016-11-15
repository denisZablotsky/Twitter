using Twitter.Models;

namespace Twitter.Data
{
    interface IUserProfileRepository
    {
        UserProfile CreateUserProfile(UserProfile userProfile);
    }
}
