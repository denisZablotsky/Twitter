using Twitter.Models;
using System;
using System.Linq;
using System.Collections.Generic;

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
            User user = context.Users.Find(tweet.UserId);
            user.Tweets = (ICollection<Tweet>)user.Tweets.OrderByDescending(x => x.CreatingDate);
            context.SaveChanges();
            return newTweet;
        }
    }
}