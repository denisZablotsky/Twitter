using System.Linq;
using Twitter.Models;

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