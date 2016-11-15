using Twitter.Models;
using System.Data.Entity;

namespace Twitter.Data
{
    public class EfDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
    }
}