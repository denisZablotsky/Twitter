using Twitter.Models;

namespace Twitter.Data
{
    public class EfUserProfileRepository : IUserProfileRepository
    {
        EfDbContext context;
        public EfUserProfileRepository()
        {
            context = new EfDbContext();
        }
        public UserProfile CreateUserProfile(UserProfile userProfile)
        {
            UserProfile newUserProfile = context.UserProfiles.Add(userProfile);
            context.SaveChanges();
            return newUserProfile;
        }
    }
}