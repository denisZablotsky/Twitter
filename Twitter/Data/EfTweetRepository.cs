using Twitter.Models;

namespace Twitter.Data
{
    public class EfTweetRepository : ITweetRepository
    {
        private EfDbContext context;
        public EfTweetRepository()
        {
            context = new EfDbContext();
        }
        public Tweet CreateTweet(Tweet tweet)
        {
            Tweet newTweet = context.Tweets.Add(tweet);
            context.SaveChanges();
            return newTweet;
        }
    }
}