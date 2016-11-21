﻿using Twitter.Models;
using System.Data.Entity;

namespace Twitter.Data
{
    public class EfDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Tweet> Tweets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Followings)
                .WithMany(x => x.Followers)
                .Map(m =>
                {
                    m.ToTable("Follow");
                    m.MapLeftKey("FollowerId");
                    m.MapRightKey("FollowingId");
                });
            Database.SetInitializer<EfDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}