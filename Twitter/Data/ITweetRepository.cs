using Twitter.Models;
using System.Linq;

namespace Twitter.Data
{
    public interface ITweetRepository
    {
        Tweet CreateTweet(Tweet tweet);
        Tweet GetTweetById(int id);
        void Like(int tweetId);
        void AddHashtag(int TweetId, Hashtag hashtag);
        IQueryable<Tweet> GetLastNews();
    }
}
