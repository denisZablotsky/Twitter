using Twitter.Models;
using System.Data.Entity;

namespace Twitter.Data
{
    public class EfDbContext: DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<Tweet> Tweets { get; set; }
    }
}