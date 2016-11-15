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

        public User GetUserById(int id)
        {
            return context.Users.Find(id);
        }

        public User GetUserByName(string name)
        {
            return context.Users.SingleOrDefault(x => x.Name == name);
        }
    }
}