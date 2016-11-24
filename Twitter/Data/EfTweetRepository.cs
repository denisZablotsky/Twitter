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

        public void AddHashtag(int TweetId, Hashtag hashtag)
        {
            Tweet tweet = context.Tweets.Find(TweetId);
            Hashtag hash = context.Hashtags.SingleOrDefault(x => x.Tag == hashtag.Tag);
            if (hash == null)
                tweet.Hashtags.Add(hashtag);
            else
                tweet.Hashtags.Add(hash);
            context.Tweets.Attach(tweet);
            context.SaveChanges();
        }

        public Tweet CreateTweet(Tweet tweet)
        {
            tweet.CreatingDate = DateTime.Now;
            Tweet newTweet = context.Tweets.Add(tweet);
            context.SaveChanges();
            return newTweet;
        }
        public Tweet GetTweetById(int id)
        {
            return context.Tweets.Find(id);
        }
        public void Like(int tweetId)
        {
            Tweet tweet = GetTweetById(tweetId);
            tweet.Likes++;
            context.SaveChanges();
        }
    }
}