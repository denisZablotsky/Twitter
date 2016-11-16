using Twitter.Models;

namespace Twitter.Services
{
    public interface ISecurityService
    {
        bool IsAuthenticate();
        bool Authenticate(string name, string password);
        void Login(User user);
        void Logout();
        User GetCurrentUser();
    }
}
