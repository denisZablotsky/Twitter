using Twitter.Models;

namespace Twitter.Data
{
    public interface ITweetRepository
    {
        Tweet CreateTweet(Tweet tweet);
        Tweet GetTweetById(int id);
        void Like(int tweetId);
    }
}
