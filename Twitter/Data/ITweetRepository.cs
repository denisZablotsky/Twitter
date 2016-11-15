using Twitter.Models;

namespace Twitter.Data
{
    interface ITweetRepository
    {
        Tweet CreateTweet(Tweet tweet);
    }
}
