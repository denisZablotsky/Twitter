using Twitter.Models;
using System;
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
            tweet.CreatingDate = DateTime.Now;
            Tweet newTweet = context.Tweets.Add(tweet);
            context.SaveChanges();
            return newTweet;
        }
    }
}