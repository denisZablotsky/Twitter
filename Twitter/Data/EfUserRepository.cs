using System.Linq;
using Twitter.Models;
using System;

namespace Twitter.Data
{
    public class EfUserRepository : IUserRepository
    {
        private EfDbContext context;
        public EfUserRepository()
        {
            context = new EfDbContext();
        }

        public IQueryable<User> Users
        {
            get
            {
                return context.Users;
            }
        }

        public User CreateUser(User user)
        {
            user.Profile.SignupData = DateTime.Now;
            user.Profile.Id = user.Id;
            context.UserProfiles.Add(user.Profile);
            User newUser = context.Users.Add(user);
            context.SaveChanges();
            return newUser;
        }

        public void Follow(int FollowerId, int FollowingId)
        {
            User currentUser = GetUserById(FollowerId);
            User user = GetUserById(FollowingId);
            currentUser.Followings.Add(user);
            context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return context.Users.Find(id);
        }

        public User GetUserByName(string name)
        {
            return context.Users.SingleOrDefault(x => x.Name == name);
        }

        public void Unfollow(int FollowerId, int FollowingId)
        {
            User currentUser = GetUserById(FollowerId);
            User user = GetUserById(FollowingId);
            currentUser.Followings.Remove(user);
            context.SaveChanges();
        }
    }
}