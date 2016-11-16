using Twitter.Models;
using System.Linq;

namespace Twitter.Data
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        User CreateUser(User user);
        User GetUserById(int id);
        User GetUserByName(string name);
        void Follow(int FollowerId, int FollowingId);
        void Unfollow(int FollowerId, int FollowingId);
    }
}
