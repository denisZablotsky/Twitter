using Twitter.Models;

namespace Twitter.Data
{
    public interface ITweetRepository
    {
        Tweet CreateTweet(Tweet tweet);
    }
}
