using Twitter.Models;
using System.Linq;

namespace Twitter.Data
{
    interface IUserRepository
    {
        IQueryable<User> Users { get; }
        User CreateUser(User user);
        User GetUserById(int id);
        User GetUserByName(string name);
    }
}
